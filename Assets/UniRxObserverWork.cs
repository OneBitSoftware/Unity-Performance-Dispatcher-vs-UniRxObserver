using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class UniRxObserverWork : MonoBehaviour
{
    public Text TextFieldToUpdate;
    private ReactiveProperty<long> _bulkReactiveHandle;

    /// <summary>
    /// Instantiates collections and subscribes callback functions
    /// </summary>
    private void Awake()
    {
        Stopwatch sw = new Stopwatch();

        _bulkReactiveHandle = new ReactiveProperty<long>(); // 1000000 iterations

        _bulkReactiveHandle
            .ObserveOn(Scheduler.MainThread)
            .Subscribe((val) =>
            {
                if (val == 0) { sw.Reset(); sw.Start(); }

                if (val == 9999999)
                {
                    sw.Stop();

                    if(TextFieldToUpdate != null)
                    TextFieldToUpdate.text = (val+1).ToString() + " item took (ms): " + sw.ElapsedMilliseconds;
                }
            });
    }

    /// <summary>
    /// Updates the bulk reactive property 10000000 times
    /// </summary>
    public void DoBulkWork_Click()
    {
        for (int i = 0; i < 10000000; i++)
        {
            _bulkReactiveHandle.Value = i;
        }
    }
}