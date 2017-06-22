using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

public class DispatcherWork : MonoBehaviour
{
    public Text TextFieldToUpdate;

    /// <summary>
    /// Enqueues 1000000 actions in the Dispatcher
    /// </summary>
    public void DoDispatcherkWork_Click()
    {
        Stopwatch sw = new Stopwatch();
        for (int i = 0; i < 10000000; i++)
        {
            Dispatcher.Instance.Enqueue((counter) =>
            {
                if (counter == 0) { sw.Reset(); sw.Start(); }
                if (counter == 9999999)
                {
                    sw.Stop();

                    if (TextFieldToUpdate != null)
                        TextFieldToUpdate.text = counter.ToString() + " item took (ms): " + sw.ElapsedMilliseconds;
                }
            }); 
        }
    }
}
