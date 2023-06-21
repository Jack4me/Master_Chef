using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public interface IHasProgress {
    public event EventHandler<OnProgressEventArgs> OnProgressBarEvent;
    public class OnProgressEventArgs : EventArgs {
        public float progressNormalize;

    }
}
