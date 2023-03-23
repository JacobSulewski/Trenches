namespace Trenches.Factories;

interface IFactory<T> {
    T Create();
}