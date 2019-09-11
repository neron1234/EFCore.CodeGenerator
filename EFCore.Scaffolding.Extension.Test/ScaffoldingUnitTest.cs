namespace EFCore.Scaffolding.Extension.Test
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using Entities;
    using WeCantSpell.Hunspell;
    using Xunit;
    using Xunit.Abstractions;

    public class ScaffoldingUnitTest
    {
        private readonly ITestOutputHelper output;

        public ScaffoldingUnitTest(ITestOutputHelper outputHelper)
        {
            this.output = outputHelper;
        }

        [Fact]
        public void Generate_entities()
        {
            DirectoryInfo di = new DirectoryInfo(Environment.CurrentDirectory);
            var _Scaffolding = di.Parent.Parent.Parent.Parent.GetFiles("_Scaffolding.xml", SearchOption.AllDirectories).FirstOrDefault();
            var list = ScaffoldingHelper.Scaffolding("Entities", "ScaffoldingDbContext", _Scaffolding.Directory.FullName);
        }

        [Fact]
        public void Database_check_typo()
        {
            var databaseModel = DbContextGenerator.GetDatabaseModel();
            bool isSuccess = true;
            foreach (var table in databaseModel.Tables)
            {
                var tableNameSuggests = this.CheckFieldAndReturnSuggestions(table.Name);
                isSuccess &= tableNameSuggests.Count == 0;
                if (tableNameSuggests.Count != 0)
                {
                    this.output.WriteLine($"Spell Wrong, Table Name: {table.Name}.");
                    this.output.WriteLine("Suggestions:");
                    this.output.WriteLine($"{string.Join(Environment.NewLine, tableNameSuggests.Where(o => !o.Contains("-")).Select(o => "     " + o.Replace(" ", "_").ToLower()))}");
                    this.output.WriteLine(new string('-', 30));
                }

                foreach (var column in table.Columns)
                {
                    var columnSuggests = this.CheckFieldAndReturnSuggestions(column.Name);
                    if (columnSuggests.Count != 0)
                    {
                        this.output.WriteLine($"Spell Wrong, Table Name: {table.Name}, Column: {column.Name}.");
                        this.output.WriteLine("Suggestions:");
                        this.output.WriteLine($"{string.Join(Environment.NewLine, columnSuggests.Where(o => !o.Contains("-")).Select(o => "     " + o.Replace(" ", "_").ToLower()))}");
                        this.output.WriteLine(new string('-', 30));
                    }

                    isSuccess &= columnSuggests.Count == 0;
                }

                Assert.True(isSuccess);
            }
        }

        private List<string> CheckFieldAndReturnSuggestions(string filed)
        {
            var directory = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent;
            var file = directory.GetFiles("en_US.dic").First();
            var dictionary = WordList.CreateFromFiles(file.FullName);

            var valuesToCheck = filed.Split('_');
            bool ok = true;
            var suggests = new List<string>();
            foreach (var item in valuesToCheck)
            {
                var isItemOk = dictionary.Check(item);
                ok &= isItemOk;
                if (!isItemOk)
                {
                    suggests = suggests.Concat(dictionary.Suggest(item)).ToList();
                }
            }

            return suggests;
        }

        [Fact]
        public void Test_insert_entity()
        {
            using (ScaffoldingDbContext testContext = new ScaffoldingDbContext())
            {
                var entity = new Student
                {
                    Name = "test",
                    Sex = "��",
                    CreateTime = DateTime.Now,
                    UpdateTime = DateTime.Now,
                };
                testContext.Student.Add(entity);
                int count = testContext.SaveChanges();
                Assert.Equal(1, count);
            }
        }
    }
}
