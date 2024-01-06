using System;
using System.Collections.Generic;
using System.IO;
using System.Web.UI;
using EO.Web;
using Newtonsoft.Json;
public partial class Default2 : Page
{
    private string filePath = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        filePath = Server.MapPath("~/App_Data/data.json");
    }
    public class Person
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
    }

    protected void btnCreate_Click(object sender, EventArgs e)
    {
        // Create a new person
        Person newPerson = new Person
        {
            FirstName = "John",
            LastName = "Doe",
            Age = 30
        };

        // Read existing data from the JSON file
        List<Person> existingData = ReadJsonFile();

        // Add the new person to the existing data
        existingData.Add(newPerson);

        // Save the updated data back to the JSON file
        WriteJsonFile(existingData);
    }

    protected void btnRead_Click(object sender, EventArgs e)
    {
        // Read existing data from the JSON file
        List<Person> existingData = ReadJsonFile();

        // Display the data (you can use GridView, Repeater, etc.)
        foreach (var person in existingData)
        {

            Response.Write("Name: {person.FirstName} {person.LastName}, Age: {person.Age}<br />");
        }
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        // Read existing data from the JSON file
        List<Person> existingData = ReadJsonFile();

        // Update the data as needed (for example, update the age of the first person)
        if (existingData.Count > 0)
        {
            existingData[0].Age = 31;
        }

        // Save the updated data back to the JSON file
        WriteJsonFile(existingData);
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        // Read existing data from the JSON file
        List<Person> existingData = ReadJsonFile();

        // Delete the first person (you can implement deletion logic based on your requirements)
        if (existingData.Count > 0)
        {
            existingData.RemoveAt(0);
        }

        // Save the updated data back to the JSON file
        WriteJsonFile(existingData);
    }

    private List<Person> ReadJsonFile()
    {
        List<Person> existingData = new List<Person>();

        // Check if the file exists
        if (File.Exists(filePath))
        {
            // Read the existing data from the file
            string json = File.ReadAllText(filePath);
            existingData = JsonConvert.DeserializeObject<List<Person>>(json);
           //var stingData = JsonConvert.DeserializeObject<Person>(json);
        }

        return existingData;
    }

    private void WriteJsonFile(List<Person> data)
    {
        // Serialize the data to JSON format
        string json = JsonConvert.SerializeObject(data, Formatting.Indented);

        // Write the JSON data to the file
        File.WriteAllText(filePath, json);
    }
}