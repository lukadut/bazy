using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bazy
{
    class Queries
    {
        public static void getCars()
        {
            projektEntities ctx = new projektEntities();
            var query = from car in ctx.cars
                        select new
                        {
                            Car = car
                        };
            CarsForm.clearDataGridView();

            foreach (var result in query)
            {
                CarsForm.fillDataGridView(result.Car.Id, result.Car.Number_plate, result.Car.Make, result.Car.Model, result.Car.Carry, result.Car.IsUsed, result.Car.Sold, result.Car.Comment);
            }
        }

        public static void getDrivers()
        {
            projektEntities ctx = new projektEntities();
            var query = from driver in ctx.drivers
                        select new
                        {
                            Driver = driver
                        };
            DriversForm.clearDataGridView();

            foreach (var result in query)
            {
                DriversForm.fillDataGridView(result.Driver.Id, result.Driver.Name,result.Driver.Surname,result.Driver.Wage,result.Driver.ADR_License,result.Driver.Employed,result.Driver.Busy,result.Driver.Comment);
            }
        }

        public static void getCargo()
        {
            projektEntities ctx = new projektEntities();
            var query = from c in ctx.cargo
                        select new
                        {
                            Cargo = c
                        };
            CargoForm.clearDataGridView();

            foreach (var result in query)
            {
                CargoForm.fillDataGridView(result.Cargo.Id,result.Cargo.Name,result.Cargo.Type,result.Cargo.ADR,result.Cargo.ADR_Class,result.Cargo.Comment);
            }
        }
        public static void getCompanies()
        {
            projektEntities ctx = new projektEntities();
            var query = from c in ctx.companies
                        join ci in ctx.cities_list on c.CityId equals ci.Id
                        join co in ctx.company_name_list on c.CompanyId equals co.Id
                        select new
                        {
                            Company = c,
                            CompanyName = co,
                            CityName = ci
                        };
            CompaniesForm.clearDataGridView();

            foreach (var result in query)
            {
                CompaniesForm.fillDataGridView(result.Company.Id, result.CompanyName.Company, result.CityName.City, result.Company.Address, result.Company.Comment);
            }
        }
        public static void getFreights()
        {
            projektEntities ctx = new projektEntities();
            var query = from fr in ctx.freights
                        join ca in ctx.cargo on fr.CargoId equals ca.Id
                        join st in ctx.companies on fr.From equals st.Id
                        join stci in ctx.cities_list on st.CityId equals stci.Id
                        join stco in ctx.company_name_list on st.CompanyId equals stco.Id
                        join de in ctx.companies on fr.To equals de.Id
                        join deci in ctx.cities_list on de.CityId equals deci.Id
                        join deco in ctx.company_name_list on de.CompanyId equals deco.Id
                        select new
                        {
                            Freight = fr,
                            Cargo = ca,
                            Start = st,
                            StartCity = stci,
                            StartCompany = stco,
                            Destination = de,
                            DestinationCity = deci,
                            DestinationCompany = deco
                        };
            FreightsForm.clearDataGridView();
            foreach (var result in query)
            {
                FreightsForm.fillDataGridView(result.Freight.Id, result.Cargo.Name,
                    result.StartCompany.Company + ", " + result.StartCity.City,
                    result.DestinationCompany.Company + ", " + result.DestinationCity.City,
                    result.Freight.ScheduledArrive, result.Freight.Amount, result.Freight.Weight, result.Freight.Comment);
            }
        }

        public static void getShipping()
        {
            projektEntities ctx = new projektEntities();
            var query = from ship in ctx.shipping
                        join car in ctx.cars on ship.CarId equals car.Id
                        join dri in ctx.drivers on ship.DriverId equals dri.Id
                        join fr in ctx.freights on ship.FreightId equals fr.Id
                        join ca in ctx.cargo on fr.CargoId equals ca.Id
                        join st in ctx.companies on fr.From equals st.Id
                        join stci in ctx.cities_list on st.CityId equals stci.Id
                        join stco in ctx.company_name_list on st.CompanyId equals stco.Id
                        join de in ctx.companies on fr.To equals de.Id
                        join deci in ctx.cities_list on de.CityId equals deci.Id
                        join deco in ctx.company_name_list on de.CompanyId equals deco.Id
                        select new
                        {
                            Shipping = ship,
                            Car = car,
                            Driver = dri,
                            Freight = fr,
                            Cargo = ca,
                            Start = st,
                            StartCity = stci,
                            StartCompany = stco,
                            Destination = de,
                            DestinationCity = deci,
                            DestinationCompany = deco
                        };
            ShippingForm.clearDataGridView();
            foreach (var result in query)
            {
                ShippingForm.fillDataGridView(result.Shipping.Id, result.Driver.Surname + " " + result.Driver.Name, result.Car.Make + " " + result.Car.Model + ", " + result.Car.Number_plate,
                    result.Cargo.Name + ": " + result.StartCompany.Company + ", " + result.StartCity.City + '\u279C' + result.DestinationCompany.Company + ", " + result.DestinationCity.City,
                    result.Shipping.DepartTime, result.Shipping.ArriveTime, result.Shipping.Delivered, result.Shipping.Comment);
            }
        }
    }
}
