using CarsCrudApi.Cars.Model;

using FluentMigrator;
using FluentMigrator.Expressions;

namespace CarsCrudApi.Data.Migrations;

[Migration(21032024)]

public class TestMigrate:Migration //Change log-uri, imi spune ce schimbari au avut loc in baza de date
{
    public override void Up()
    {
        Execute.Script(@"./Data/scripts/data.sql");
    }
    public override void Down()
    {

    }
}
