namespace Trenches.Factories;

interface IFactory<T>
{
    T Create();
}

interface IFactory<T, U>
{
    T Create(U param);
}