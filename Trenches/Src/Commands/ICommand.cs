namespace Trenches.Commands;
interface ICommand
{
    void Execute();
}
interface ICommand<T>
{
    void Execute(T param);
}