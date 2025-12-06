using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using CASO2.Data;
using CASO2.Data.Repositories;
using CASO2.Data.Repository;

namespace CASO2.Core
{
    // Pattern: Repository (existing)
    // Pattern: Aca se usa el chain of responsability para abarcar los pasos a realizar desde el 1 al 5, en este se implementa primero las interfaces
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


        private bool ExecuteStage1()
        {
            // Eliminar registros con China/Rusia
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

        // Paso 2 2:
        // - Remover informacion sensible y pws
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

        // Paso 3
        // modificar correos y salario
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
                    if (!string.IsNullOrWhiteSpace(u.Email))
                    {
                        u.Email = MaskEmail(u.Email);
                    }

                    if (!string.IsNullOrWhiteSpace(u.Address))
                    {
                        u.Address = NormalizeAddress(u.Address);
                    }

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
                    // aca se debe de modificar para poder realizar el paso 3.4
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

        // Paso 4
        //Completar y cambiar estados a Ready
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

            s = Regex.Replace(s, @"\bP\.?\s*O\.?\s*\.?\s*Box\s*\d+\s*,\s*\d+\s*,?\s*", "", RegexOptions.IgnoreCase);
            s = Regex.Replace(s, @"\bAP\s*#\s*\d[\d\-]*\b", "", RegexOptions.IgnoreCase);
            s = Regex.Replace(s, @"^\s*\d+(-\d+)?\s+", "", RegexOptions.IgnoreCase);

            s = s.Trim();
            s = Regex.Replace(s, @"^[,.\s]+", "");
            return string.IsNullOrEmpty(s) ? address : s;
        }

        private static string MapSalary(string salary)
        {
            var num = ParseSalaryToDouble(salary);
            if (!num.HasValue) return salary;

            var v = num.Value;

            if (v >= 5000 && v <= 6500) return "44";
            if (v > 6500 && v <= 8000) return "33";
            if (v > 8000 && v <= 9500) return "22";
            if (v > 9500) return "11";

            return salary;
        }

        private static double? ParseSalaryToDouble(string input)
        {
            if (string.IsNullOrWhiteSpace(input)) return null;
            var s = input.Trim().ToLowerInvariant();

            var m = Regex.Match(s, @"(\d+(\.\d+)?)\s*(k)?", RegexOptions.IgnoreCase);
            if (!m.Success) return null;

            if (!double.TryParse(m.Groups[1].Value, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out var baseNum))
                return null;

            var isK = m.Groups[3].Success && !string.IsNullOrEmpty(m.Groups[3].Value);
            return isK ? baseNum * 1000.0 : baseNum;
        }
    }
}

