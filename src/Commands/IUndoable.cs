namespace Trenches.Commands;
interface IUndoable : ICommand {
    void Undo();
}