using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using FB.Data;
using FB.Data.Repositories;
using FB.Data.Repository;

namespace FB.Core
{
    // Pattern: Repository (existing)
    // Pattern: Chain of Responsibility para los pasos 1 al 5
    public class UserDetailBusiness
    {
        private readonly IRepositoryUserDetail _repository;

        public UserDetailBusiness()
        {
            _repository = new RepositoryUserDetail();
        }

        public IEnumerable<UserDetail> GetUserDetails(int id = 0)
        {
            return id <= 0
                ? _repository.GetAll()
                : new List<UserDetail>() { _repository.GetById(id) };
        }

        public UserDetail GetByEmail(string email)
        {
            if (string.IsNullOrEmpty(email)) return null;

            return _repository
                    .GetAll()
                    .FirstOrDefault(u =>
                        u.Email != null &&
                        u.Email.ToLower() == email.ToLower());
        }

        public bool ValidateCredentials(string email, string password)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
                return false;

            var user = _repository
                .GetAll()
                .FirstOrDefault(u =>
                    u.Email != null &&
                    u.Email.ToLower() == email.ToLower() &&
                    u.Password == password);

            return user != null;
        }

        public bool IsStageCompleted(int stage)
        {
            var items = _repository.GetAll().ToList();
            if (!items.Any()) return true;

            foreach (var u in items)
            {
                if (string.IsNullOrWhiteSpace(u.Text)) return false;
                if (!int.TryParse(u.Text, out var t)) return false;
                if (t < stage) return false;
            }
            return true;
        }

        public (bool Success, string Message) ExecuteStage(int stage)
        {
            try
            {
                switch (stage)
                {
                    case 1:
                        return (ExecuteStage1(), "Paso 1 listo.");
                    case 2:
                        if (!IsStageCompleted(1))
                            return (false, "El paso 1 debe de estar completo primero.");
                        return (ExecuteStage2(), "Paso 2 listo.");
                    case 3:
                        if (!IsStageCompleted(2))
                            return (false, "El paso 2 debe de estar completo primero.");
                        return (ExecuteStage3(), "Paso 3 listo.");
                    case 4:
                        if (!IsStageCompleted(3))
                            return (false, "El paso 3 debe de estar completo primero.");
                        return (ExecuteStage4(), "Paso 4 listo.");
                    default:
                        return (false, "Unknown stage.");
                }
            }
            catch (Exception ex)
            {
                return (false, "Error executing stage: " + ex.Message);
            }
        }

        // Paso 1:
        // Eliminar registros con China/Rusia y marcar Text = "1"
        private bool ExecuteStage1()
        {
            try
            {
                _repository.DeleteByCountry("Russia");
                _repository.DeleteByCountry("China");

                var items = _repository.GetAll().ToList();
                foreach (var u in items)
                {
                    u.Text = "1";
                }

                _repository.Save();
                return true;
            }
            catch
            {
                return false;
            }
        }

        // Paso 2:
        // Remover informacion sensible y passwords
        private bool ExecuteStage2()
        {
            try
            {
                var items = _repository.GetAll().ToList();
                foreach (var u in items)
                {
                    u.Phone = null;
                    u.Password = null;
                    u.Text = "2";
                }

                _repository.Save();
                return true;
            }
            catch
            {
                return false;
            }
        }

        // Paso 3:
        // Modificar correos, direcciones, asignar NumberRange y mapear Salary
        private bool ExecuteStage3()
        {
            try
            {
                var items = _repository.GetAll().ToList();

                int maxExisting = 0;
                try
                {
                    maxExisting = items
                        .Where(x => x.NumberRange != null && x.NumberRange > 0)
                        .Select(x => (int)x.NumberRange)
                        .DefaultIfEmpty(0)
                        .Max();
                }
                catch
                {
                    maxExisting = items.Select(x => 0).DefaultIfEmpty(0).Max();
                }

                foreach (var u in items)
                {
                    // 3.1 - Enmascarar correo
                    if (!string.IsNullOrWhiteSpace(u.Email))
                    {
                        u.Email = MaskEmail(u.Email);
                    }

                    // 3.2 - Normalizar dirección
                    if (!string.IsNullOrWhiteSpace(u.Address))
                    {
                        u.Address = NormalizeAddress(u.Address);
                    }

                    // 3.3 - Asignar correlativo a NumberRange si es 0
                    try
                    {
                        if (u.NumberRange == 0)
                        {
                            maxExisting++;
                            u.NumberRange = maxExisting;
                        }
                    }
                    catch
                    {
                    }

                    // 3.4 - Mapear Salary a 4 / 3 / 2 / 1 según rango
                    if (!string.IsNullOrWhiteSpace(u.Salary))
                    {
                        var mapped = MapSalary(u.Salary);
                        if (!string.IsNullOrEmpty(mapped))
                            u.Salary = mapped;
                    }

                    u.Text = "3";
                }

                _repository.Save();
                return true;
            }
            catch
            {
                return false;
            }
        }

        // Paso 4:
        // Cambiar estados a Ready y Text = "4"
        private bool ExecuteStage4()
        {
            try
            {
                var items = _repository.GetAll().ToList();
                foreach (var u in items)
                {
                    u.Status = "Ready";
                    u.Text = "4";
                }

                _repository.Save();
                return true;
            }
            catch
            {
                return false;
            }
        }

        // --- Helpers ---

        private static string MaskEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email)) return email;
            var at = email.IndexOf('@');
            if (at <= 0) return "*****@";
            var domain = email.Substring(at + 1);
            return "*****@" + domain;
        }

        private static string NormalizeAddress(string address)
        {
            if (string.IsNullOrWhiteSpace(address)) return address;
            var s = address.Trim();

            // Remover PO Box, AP#, prefijos numéricos, etc.
            s = Regex.Replace(s, @"\bP\.?\s*O\.?\s*\.?\s*Box\s*\d+\s*,\s*\d+\s*,?\s*", "", RegexOptions.IgnoreCase);
            s = Regex.Replace(s, @"\bAP\s*#\s*\d[\d\-]*\b", "", RegexOptions.IgnoreCase);
            s = Regex.Replace(s, @"^\s*\d+(-\d+)?\s+", "", RegexOptions.IgnoreCase);

            s = s.Trim();
            s = Regex.Replace(s, @"^[,.\s]+", "");
            return string.IsNullOrEmpty(s) ? address : s;
        }


        /// Mapea el salario original a:
        ///  4  -> entre 5k y 6.5k
        ///  3  -> entre 6.5k y 8k
        ///  2  -> entre 8k y 9.5k
        ///   1  -> mayor o igual a 9.5k
        /// Se devuelve el valor original si no se puede interpretar el salario

        private static string MapSalary(string salaryRaw)
        {
            if (string.IsNullOrWhiteSpace(salaryRaw))
                return salaryRaw;

            // Se normaliza para analizar texto
            var s = salaryRaw.Trim().ToLowerInvariant();

            // Caso 1: El salario ya viene como rango en texto:
            // "5k-6.5k", "5k a 6.5k", "entre 5k y 6.5k", etc.
            if (s.Contains("5k") && s.Contains("6.5k"))
                return "4";

            if (s.Contains("6.5k") && s.Contains("8k"))
                return "3";

            if (s.Contains("8k") && s.Contains("9.5k"))
                return "2";

            // Mayor o igual a 9.5k (ej. "9.5k+", ">=9.5k", "9.5k o más")
            if (s.Contains("9.5k"))
                return "1";

            // Caso 2: El salario viene como un número: 5000, 6500, 8000, 9500, etc. ---
            // Limpiamos símbolos comunes
            s = s.Replace("$", "")
                 .Replace("₡", "")
                 .Replace(",", "")
                 .Replace(" ", "");

            decimal numericSalary;

            // Si termina en 'k', se interpreta como miles: 5k -> 5000, 9.5k -> 9500
            if (s.EndsWith("k"))
            {
                var numberPart = s.Substring(0, s.Length - 1); // quitamos la 'k'

                if (decimal.TryParse(numberPart, NumberStyles.Any, CultureInfo.InvariantCulture, out var baseK))
                {
                    numericSalary = baseK * 1000m;
                    return MapSalaryNumeric(numericSalary);
                }
            }

            // Se intenta parsear directamente como número
            if (decimal.TryParse(s, NumberStyles.Any, CultureInfo.InvariantCulture, out numericSalary))
            {
                return MapSalaryNumeric(numericSalary);
            }

            // Se devuelve el valor original si no se puede interpretar el salario
            return salaryRaw;
        }

        /// Aplica las reglas de rango a un valor numérico de salario.
        private static string MapSalaryNumeric(decimal salary)
        {
            // Entre 5k y 6.5k → 4
            if (salary >= 5000m && salary < 6500m)
                return "4";

            // Entre 6.5k y 8k → 3
            if (salary >= 6500m && salary < 8000m)
                return "3";

            // Entre 8k y 9.5k → 2
            if (salary >= 8000m && salary < 9500m)
                return "2";

            // Mayor o igual a 9.5k → 1
            if (salary >= 9500m)
                return "1";

            // Menor a 5k o fuera de rango: no asignamos nada
            return null;
        }
    }
}