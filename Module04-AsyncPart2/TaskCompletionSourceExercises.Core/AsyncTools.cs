using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace TaskCompletionSourceExercises.Core
{
    public static class AsyncTools
    {
        public static Task<string> RunProgramAsync(string path, string args = "")
        {
            var process = new Process();
            process.EnableRaisingEvents = true;
            process.StartInfo = new ProcessStartInfo(path, args)
            {
                RedirectStandardOutput = true,
                RedirectStandardError = true
            };

            var tcs = new TaskCompletionSource<string>(TaskCreationOptions.RunContinuationsAsynchronously);
            process.Exited += async (sender, eventArgs) =>
            {
                var senderProcess = sender as Process;
                if (senderProcess is null)
                    return;
                if (senderProcess.ExitCode != 0)
                {
                    var output = await process.StandardError.ReadToEndAsync();
                    tcs.SetException(new Exception(output));
                }
                else
                {
                    var output = await process.StandardOutput.ReadToEndAsync();
                    tcs.SetResult(output);
                }
            };

            process.Start();

            return tcs.Task;
        }
    }
}