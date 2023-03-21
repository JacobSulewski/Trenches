namespace Trenches.Factories;
interface IFactory<out TBase>
{
    TBase Create<T>();
}