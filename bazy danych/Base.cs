using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace bazy_danych
{
    public class Base
    {
        private string Server { get; set; }
        private string DataBase { get; set; }
        private string UID { get; set; }
        private string Password { get; set; }
        private string Charset { get; set; }
        public MySqlConnection MySqlConnector;
        public List<Drivers> DriversList;
        public List<Cars> CarsList;
        public List<FreightsList> FreightsListList;
        /// <summary>
        /// ??????
        /// </summary>
        public List<CitiesList> CitiesListList;
        public List<CompanyNamesList> CompanyNamesListList;
        public List<Companies> CompaniesList;


        //MySqlCommand cmd;
       // private MySqlCommand cmd1;


        public Base()
        {
            ConnectToDataBase();
            DriversList = new List<Drivers>();
            LoadDrivers();
            CarsList = new List<Cars>();
            LoadCars();
            FreightsListList = new List<FreightsList>();
            LoadFreightsList();
            CitiesListList = new List<CitiesList>();
            LoadCitiesListList();
            CompanyNamesListList = new List<CompanyNamesList>();
            LoadCompaniesListList();
            CompaniesList = new List<Companies>();
            LoadCompanies();

            
        }

        /// <summary>
        /// Trzeba uzyc try catch
        /// </summary>
        public void ConnectToDataBase()
        {
            Server = "localhost";
            DataBase = "projekt";
            UID = "root";
            Charset = "utf8";
            Password = "";
            string Connection = "SERVER=" + Server + ";" + "DATABASE=" + DataBase + ";" + "UID=" + UID + ";" + "PASSWORD=" + Password + ";" + "CHARSET=" + Charset;
            MySqlConnector = new MySqlConnection(Connection);
        }

        /// <summary>
        /// tez trzeba uzyc try catch
        /// </summary>
        public void LoadDrivers()
        {
            //string result = "";
            MySqlCommand cmd;
            cmd = MySqlConnector.CreateCommand();
            cmd.CommandText = "SELECT * FROM drivers";
            MySqlConnector.Open();
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                string name = "";
                if (!reader.IsDBNull(1))
                {
                    name = reader.GetString(1);
                }
                DriversList.Add(new Drivers(reader.GetUInt32("Id"), name /*reader.GetString("Name")*/, reader.GetString("Surname"), reader.GetUInt32("Wage"), reader.GetBoolean("ADR_License"), reader.GetBoolean("Employed"), reader.GetBoolean("Busy"), reader.GetString("Comment")));
                //result += "\n" + reader.GetString("Name") + "	" +reader.GetString("Surname") + "	" +reader.GetUInt32("Wage") + "	" +
                //    reader.GetBoolean("ADR_License") + "	" +reader.GetBoolean("Employed") + "	" +reader.GetString("Comment");
            }
            MySqlConnector.Close();
            //return result;

        }
        public void AddDriver(string name, string surname, uint wage, bool adr, bool employed, string comment)
        {
            MySqlCommand cmd;
            cmd = MySqlConnector.CreateCommand();
            cmd.CommandText = "INSERT INTO drivers(name,surname,wage,adr_license,employed,comment) VALUES (@Name,@Surname,@Wage,@Adr,@Employed,@Comment)";
            //cmd.CommandText = "UPDATE drivers SET name=@Name, surname=@Surname, wage=@Wage, adr_license=@Adr, employed=@Employed, comment=@Comment WHERE id = @Id";
            cmd.Parameters.AddWithValue("@Name", name);
            cmd.Parameters.AddWithValue("@Surname", surname);
            cmd.Parameters.AddWithValue("@Wage", wage);


            if (adr)
            {
                cmd.Parameters.AddWithValue("@Adr", "True");
            }
            else
            {
                cmd.Parameters.AddWithValue("@Adr", "False");
            }
            if (employed)
            {
                cmd.Parameters.AddWithValue("@Employed", "True");
            }
            else
            {
                cmd.Parameters.AddWithValue("@Employed", "False");
            }
            cmd.Parameters.AddWithValue("@Comment", comment);
            MySqlConnector.Open();
            cmd.ExecuteNonQuery();
            uint Id = (uint)cmd.LastInsertedId;
            DriversList.Add(new Drivers(Id, name, surname, wage, adr, employed, false, comment));
            MySqlConnector.Close();

        }

        public void UpdateDrivers(int IdList, int IdBase)
        {
            MySqlCommand cmd;
            cmd = MySqlConnector.CreateCommand();
            cmd.CommandText = "UPDATE drivers SET name=@Name, surname=@Surname, wage=@Wage, adr_license=@Adr, employed=@Employed, comment=@Comment WHERE id = @Id";
            cmd.Parameters.AddWithValue("@Name", DriversList[IdList].Name);
            cmd.Parameters.AddWithValue("@Surname", DriversList[IdList].Surname);
            cmd.Parameters.AddWithValue("@Wage", DriversList[IdList].Wage);


            if (DriversList[IdList].Adr)
            {
                cmd.Parameters.AddWithValue("@Adr", "True");
            }
            else
            {
                cmd.Parameters.AddWithValue("@Adr", "False");
            }
            if (DriversList[IdList].Employed)
            {
                cmd.Parameters.AddWithValue("@Employed", "True");
            }
            else
            {
                cmd.Parameters.AddWithValue("@Employed", "False");
            }
            cmd.Parameters.AddWithValue("@Comment", DriversList[IdList].Comment);
            cmd.Parameters.AddWithValue("@Id", (IdBase));
            MySqlConnector.Open();
            cmd.ExecuteNonQuery();
            MySqlConnector.Close();
        }


        public void LoadCars()
        {
            //string result = "";
            MySqlCommand cmd;
            cmd = MySqlConnector.CreateCommand();
            cmd.CommandText = "SELECT * FROM cars";
            MySqlConnector.Open();
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                CarsList.Add(new Cars(reader.GetUInt32("Id"), reader.GetString("Number_plate"), reader.GetString("Make"), reader.GetString("Model"), reader.GetUInt32("Carry"), reader.GetBoolean("IsUsed"), reader.GetBoolean("Sold"), reader.GetString("Comment")));
                //result += "\n" + reader.GetString("Name") + "	" +reader.GetString("Surname") + "	" +reader.GetUInt32("Wage") + "	" +
                //    reader.GetBoolean("ADR_License") + "	" +reader.GetBoolean("Employed") + "	" +reader.GetString("Comment");
            }
            MySqlConnector.Close();
        }

        public void AddCar(string numberPlate, string make, string model, uint carry, bool isUsed, bool sold, string comment)
        {
            MySqlCommand cmd;
            cmd = MySqlConnector.CreateCommand();
            cmd.CommandText = "INSERT INTO cars(Number_plate,make,model,carry,isused,sold,comment) VALUES (@Plate,@Make,@Model,@Carry,@IsUsed,@Sold,@Comment)";
            //cmd.CommandText = "UPDATE drivers SET name=@Name, surname=@Surname, wage=@Wage, adr_license=@Adr, employed=@Employed, comment=@Comment WHERE id = @Id";
            cmd.Parameters.AddWithValue("@Plate", numberPlate);
            cmd.Parameters.AddWithValue("@Make", make);
            cmd.Parameters.AddWithValue("@Model", model);
            cmd.Parameters.AddWithValue("@Carry", carry);


            if (isUsed)
            {
                cmd.Parameters.AddWithValue("@IsUsed", "True");
            }
            else
            {
                cmd.Parameters.AddWithValue("@IsUsed", "False");
            }
            if (sold)
            {
                cmd.Parameters.AddWithValue("@Sold", "True");
            }
            else
            {
                cmd.Parameters.AddWithValue("@Sold", "False");
            }
            cmd.Parameters.AddWithValue("@Comment", comment);
            try
            {
                MySqlConnector.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception exc)
            {
                System.Console.WriteLine(exc.Message);
            }
            uint Id = (uint)cmd.LastInsertedId;
            CarsList.Add(new Cars(Id, numberPlate, make, model, carry, isUsed, sold, comment));
            MySqlConnector.Close();

        }

        public void UpdateCars(int IdList, int IdBase)
        {
            MySqlCommand cmd;
            cmd = MySqlConnector.CreateCommand();
            cmd.CommandText = "UPDATE Cars SET Model=@Model, Make=@Make, Carry=@Carry, Number_plate=@Plate, Sold=@Sold, comment=@Comment WHERE id = @Id";
            cmd.Parameters.AddWithValue("@Model", CarsList[IdList].Model);
            cmd.Parameters.AddWithValue("@Make", CarsList[IdList].Make);
            cmd.Parameters.AddWithValue("@Carry", CarsList[IdList].Carry);



            cmd.Parameters.AddWithValue("@Plate", CarsList[IdList].Plate);


            if (CarsList[IdList].Sold)
            {
                cmd.Parameters.AddWithValue("@Sold", "True");
            }
            else
            {
                cmd.Parameters.AddWithValue("@Sold", "False");
            }
            cmd.Parameters.AddWithValue("@Comment", CarsList[IdList].Comment);
            cmd.Parameters.AddWithValue("@Id", (IdBase));
            MySqlConnector.Open();
            cmd.ExecuteNonQuery();
            MySqlConnector.Close();
        }


        public void LoadFreightsList()
        {
            //string result = "";
            MySqlCommand cmd;
            cmd = MySqlConnector.CreateCommand();
            cmd.CommandText = "SELECT * FROM freights_list";
            MySqlConnector.Open();
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                FreightsListList.Add(new FreightsList(reader.GetUInt32("Id"), reader.GetString("Name"), reader.GetString("Type"), reader.GetString("ADR_class"), reader.GetBoolean("ADR"), reader.GetString("Comment")));
                //Console.Write(reader.GetFieldType(4));
                //Console.WriteLine(Functions.ExpADR(reader.GetString(4))[1]);
                //CarsList.Add(new Cars(reader.GetUInt32("Id"), reader.GetString("Number_plate"), reader.GetString("Make"), reader.GetString("Model"), reader.GetUInt32("Carry"), reader.GetBoolean("IsUsed"), reader.GetBoolean("Sold"), reader.GetString("Comment")));
                //result += "\n" + reader.GetString("Name") + "	" +reader.GetString("Surname") + "	" +reader.GetUInt32("Wage") + "	" +
                //    reader.GetBoolean("ADR_License") + "	" +reader.GetBoolean("Employed") + "	" +reader.GetString("Comment");
            }
            MySqlConnector.Close();
        }
        public void UpdateFreightsList(int IdList, int IdBase)
        {
            MySqlCommand cmd;
            cmd = MySqlConnector.CreateCommand();
            cmd.CommandText = "UPDATE Freights_list SET Name=@Name, Type=@Type, ADR=@ADR, ADR_Class=@Class, comment=@Comment WHERE id = @Id";
            cmd.Parameters.AddWithValue("@Name", FreightsListList[IdList].Name);
            cmd.Parameters.AddWithValue("@Type", FreightsListForm.Types[Functions.FindStringIndex(FreightsListForm.TypesPL,FreightsListList[IdList].Type)]);

            string adrClass = "";
            int i = 0;
            foreach (var item in FreightsListList[IdList].AdrClass)
            {
                if (item)
                {
                    adrClass += Functions.classes[i] + ",";
                }
                i++;
            }
            if (adrClass.Length > 0)
                adrClass.Remove(adrClass.Length - 1, 1);
            else
                FreightsListList[IdList].Adr = false;

            cmd.Parameters.AddWithValue("@Class", adrClass);


            if (FreightsListList[IdList].Adr)
            {
                cmd.Parameters.AddWithValue("@ADR", "True");
            }
            else
            {
                cmd.Parameters.AddWithValue("@ADR", "False");
            }
            cmd.Parameters.AddWithValue("@Comment", FreightsListList[IdList].Comment);
            cmd.Parameters.AddWithValue("@Id", (IdBase));
            MySqlConnector.Open();
            cmd.ExecuteNonQuery();
            MySqlConnector.Close();
        }
        public void AddFreightsList(string name, string type, string adrClass, bool adr, string comment)
        {
            MySqlCommand cmd;
            cmd = MySqlConnector.CreateCommand();
            cmd.CommandText = "INSERT INTO Freights_list(Name,Type,adr,ADR_Class,Comment) VALUES (@Name,@Type,@adr,@adrClass,@Comment)";

            cmd.Parameters.AddWithValue("@Name", name);
            cmd.Parameters.AddWithValue("@Type", type);
            cmd.Parameters.AddWithValue("@Comment", comment);
            cmd.Parameters.AddWithValue("@adrClass", adrClass);

            if (adr)
            {
                cmd.Parameters.AddWithValue("@adr", "True");
            }
            else
            {
                cmd.Parameters.AddWithValue("@adr", "False");
            }
            
            try
            {
                MySqlConnector.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception exc)
            {
                System.Console.WriteLine(exc.Message);
            }
            uint Id = (uint)cmd.LastInsertedId;
            FreightsListList.Add(new FreightsList(Id, name, type,adrClass,adr,comment));
            MySqlConnector.Close();
        }
        public void LoadCitiesListList()
        {
            MySqlCommand cmd;
            cmd = MySqlConnector.CreateCommand();
            cmd.CommandText = "SELECT * FROM cities_list";
            MySqlConnector.Open();
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                CitiesListList.Add(new CitiesList(reader.GetUInt32("Id"), reader.GetString("City")));
            }
            MySqlConnector.Close();
        }
        public void LoadCompaniesListList()
        {
            MySqlCommand cmd;
            cmd = MySqlConnector.CreateCommand();
            cmd.CommandText = "SELECT * FROM company_name_list";
            MySqlConnector.Open();
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                CompanyNamesListList.Add(new CompanyNamesList(reader.GetUInt32("Id"), reader.GetString("Company")));
            }
            MySqlConnector.Close();
        }
        public void LoadCompanies()
        {
            MySqlCommand cmd;
            cmd = MySqlConnector.CreateCommand();
            cmd.CommandText = "SELECT * FROM companies";
            MySqlConnector.Open();
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                int city = reader.GetInt32("CityId");
                int companyname = reader.GetInt32("CompanyId");
                CompaniesList.Add(new Companies(reader.GetUInt32("Id"),CitiesListList[Functions.FindCitiesList(city,CitiesListList)],CompanyNamesListList[Functions.FindCompanyNamesList(companyname,CompanyNamesListList)],reader.GetString("Addres"), reader.GetString("Comment")));
            }
            MySqlConnector.Close();
        }
        
    }
}
