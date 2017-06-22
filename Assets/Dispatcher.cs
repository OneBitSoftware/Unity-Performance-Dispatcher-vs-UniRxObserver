using System.Collections.Generic;
using System;

/// <summary>
/// Coordinates the actions queue and invokes pending actions
/// </summary>
public class Dispatcher
{

    /// <summary>
    /// This list contains all pending functions to be executed.
    /// All operations must be thread-safe, thus it should stay private.
    /// </summary>
    private List<Action<int>> PendingActionsQueue = new List<Action<int>>();

    private static Dispatcher _instance;

    // Typical Unity design for a static instance makes this a quasi-singleton
    public static Dispatcher Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new Dispatcher(); // Instantiate singleton on first use.
            }

            return _instance;
        }
    }

    /// <summary>
    /// Schedule code for execution in the main-thread. 
    /// </summary>
    /// <param name="fn">The Action to be executed</param>
    public void Enqueue(Action<int> fn)
    {
        if (PendingActionsQueue == null) return; // defensive programming

        lock (PendingActionsQueue) // Ensure thread safety
        {
            PendingActionsQueue.Add(fn);
        }
    }

    /// <summary>
    /// Invoke (execute) all pending actions on the main thread 
    /// </summary>
    public void InvokeAllPending()
    {
        if (PendingActionsQueue == null) return; // defensive programming
        if (PendingActionsQueue.Count < 1) return;

        lock (PendingActionsQueue) // Ensure thread safety
        {
            for (int i = 0; i < PendingActionsQueue.Count; i++)
            {
                PendingActionsQueue[i](i); // Invoke the action and pass counter.
            }

            PendingActionsQueue.Clear(); // Clear the pending list.
        }
    }

    /// <summary>
    /// Invoke the oldest action added to the queue. Executed on the main thread 
    /// </summary>
    public void InvokeOne()
    {
        if (PendingActionsQueue == null) return;
        if (PendingActionsQueue.Count < 1) return;

        lock (PendingActionsQueue) // Ensure thread safety
        {
            var action = PendingActionsQueue[0]; // takes the oldest item added
            action(0); // Invoke the action.
            PendingActionsQueue.RemoveAt(0);
        }
    }
}