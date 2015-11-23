using Rhino;
using Rhino.Commands;

namespace PacMan
{
    [System.Runtime.InteropServices.Guid("1d80b872-fdd5-4b3f-8fae-e751bf062d83")]
    public class PacManCommand : Command
    {
        public PacManCommand()
        {
            // Rhino only creates one instance of each command class defined in a
            // plug-in, so it is safe to store a refence in a static property.
            Instance = this;
        }

        ///<summary>The only instance of this command.</summary>
        public static PacManCommand Instance
        {
            get; private set;
        }

        ///<returns>The command name as it appears on the Rhino command line.</returns>
        public override string EnglishName => "Pacman";

     
        protected override Result RunCommand(RhinoDoc doc, RunMode mode)
        {
            Game.Start();
            return Result.Success;
        }
    }
}
