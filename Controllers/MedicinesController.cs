using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Pharmacy_Management_System.Models;
using System;
using System.Data;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace Pharmacy_Management_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicinesController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public MedicinesController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public JsonResult Get()
        {
            string query = @"
                            select MedicineId, MedicineName, DateOfManufacture, DateOfExpiry,MedicineQuantity,MedicinePricePerUnit from
                            dbo.Medicines
                            ";
            //here we got data in the data table object
            DataTable table = new DataTable();

            //below are the code to execute sql queries

            string sqlDataSource = _configuration.GetConnectionString("PharmacyAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    //filling the data in the data table using sql datareader
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }
            //finally returning this data in json format
            return new JsonResult(table);
        }

        [HttpPost]
        public JsonResult Post(Medicines med)
        {
            string query = @"
                            insert into dbo.Medicines 
                            (MedicineName, DateOfManufacture, DateOfExpiry,
                                MedicineQuantity,MedicinePricePerUnit) values
                               (@MedicineName, @DateOfManufacture, @DateOfExpiry,
                                @MedicineQuantity,@MedicinePricePerUnit)
                            
                            ";
            
            DataTable table = new DataTable();

           

            string sqlDataSource = _configuration.GetConnectionString("PharmacyAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {

                    

                    myCommand.Parameters.AddWithValue("@MedicineName", med.MedicineName);
                    myCommand.Parameters.AddWithValue("@DateOfManufacture", med.DateOfManufacture);
                    myCommand.Parameters.AddWithValue("@DateOfExpiry", med.DateOfExpiry);
                    myCommand.Parameters.AddWithValue("@MedicineQuantity", med.MedicineQuantity);
                    myCommand.Parameters.AddWithValue("@MedicinePricePerUnit", med.MedicinePricePerUnit);
                    myReader = myCommand.ExecuteReader();
                    //filling the data in the data table using sql datareader
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }
            //finally returning this data in json format
            return new JsonResult("Medicine Added Successfully");
        }

        [HttpPut]
        public JsonResult Put(Medicines med)
        {
            string query = @"
                            update  dbo.Medicines
                            set
                             
                            MedicineName=@MedicineName,
                            DateOfManufacture=@DateOfManufacture,
                            DateOfExpiry=@DateOfExpiry,
                            MedicineQuantity=@MedicineQuantity,
                            MedicinePricePerUnit=@MedicinePricePerUnit
                            where MedicineId=@MedicineId
                             ";

            DataTable table = new DataTable();



            string sqlDataSource = _configuration.GetConnectionString("PharmacyAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@MedicineId", med.MedicineId);
                    myCommand.Parameters.AddWithValue("@MedicineName", med.MedicineName);
                    myCommand.Parameters.AddWithValue("@DateOfManufacture", med.DateOfManufacture);
                    myCommand.Parameters.AddWithValue("@DateOfExpiry", med.DateOfExpiry);
                    myCommand.Parameters.AddWithValue("@MedicineQuantity", med.MedicineQuantity);
                    myCommand.Parameters.AddWithValue("@MedicinePricePerUnit", med.MedicinePricePerUnit);
                    myReader = myCommand.ExecuteReader();
                    //filling the data in the data table using sql datareader
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }
            //finally returning this data in json format
            return new JsonResult("Medicine Details Updated Successfully");
        }
        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            string query = @"
                           delete from dbo.Medicines
                            where MedicineId=@MedicineId
                            ";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("PharmacyAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@MedicineId", id);

                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Medicine Deleted Successfully");
        }

    }

}
