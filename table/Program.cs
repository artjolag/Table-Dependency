using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using KNLibraries;
using table;
using TableDependency;
using TableDependency.Enums;
using TableDependency.SqlClient;
using TableDependency.EventArgs;

public class Program
{
    private static string _con= MySqlServer.GenerateConnectionString(@"INFORMATIKALAB\sql2014", "ArtjolaLogiPOS");
   
    public static void Main()
    {
        // The mapper object is used to map model properties that do not have a corresponding table column name.
        // In case all properties of your model have same name of table columns, you can avoid to use the mapper.
        /*var mapper = new ModelToTableMapper<Customer>();
        mapper.AddMapping(c => c.Surname, "Second Name");
        mapper.AddMapping(c => c.Name, "First Name");*/

        // Here - as second parameter - we pass table name: this is necessary only if the model name is 
        // different from table name (in our case we have Customer vs Customers). 
        // If needed, you can also specifiy schema name.
        using (var tableDependency = new SqlTableDependency<Warranty1>(_con, tableName: "Warranty"))
        {
            tableDependency.OnChanged += TableDependency_Changed;
            tableDependency.OnError += TableDependency_OnError;
            
            tableDependency.Start();
            Console.WriteLine("Waiting to recive notification...");
            Console.WriteLine("Press a key to exit");
            Console.ReadKey();

            tableDependency.Stop();
        }
    }

    public static void TableDependency_Changed(object sender, RecordChangedEventArgs<Warranty1> e)
    {
        Console.WriteLine(Environment.NewLine);
        if (e.ChangeType != ChangeType.None)
        {
            var changedEntity = e.Entity;
            Console.WriteLine("DML operation: " + e.ChangeType);
            Console.WriteLine("ID: " + changedEntity.ID);
            Console.WriteLine("DocumentDate: " + changedEntity.DocumentDate);
            Console.WriteLine("DocumentNo: " + changedEntity.DocumentNo);
            Console.WriteLine("ItemNo: " + changedEntity.ItemNo);
            Console.WriteLine("LineNo: " + changedEntity.LineNo);
            Console.WriteLine("Serial: " + changedEntity.Serial);
            Console.WriteLine("Description: " + changedEntity.Description);
            Console.WriteLine("Warranty: " + changedEntity.Warranty);
            Console.WriteLine("Export: " + changedEntity.Export);
            Console.WriteLine("ExportCrm: " + changedEntity.ExportCrm);
            Console.WriteLine(Environment.NewLine);
        }
      
    }

    static void TableDependency_OnError(object sender, ErrorEventArgs e)
    {
        Exception ex = e.Error;
        throw ex;
    }
}