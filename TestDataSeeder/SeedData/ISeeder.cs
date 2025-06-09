namespace TestDataSeeder.SeedData;

internal interface ISeeder<T>
{
    Task<T> SeedAsync();
}
