﻿using System.Media;
using System.Windows.Forms;

namespace GameJam
{
    class AudioManager
    {
        public static bool canPlay = true;
        public static void PlaySound(System.IO.UnmanagedMemoryStream soundFile)
        {
            if (!canPlay) return;
            SoundPlayer newSoundEffect = new SoundPlayer(soundFile);
            newSoundEffect.Play();
        }
        public static void PlayMusic()
        {
            canPlay = false;
            Timer MyTimer = new Timer();
            MyTimer.Interval = (2000);
            MyTimer.Tick += (sender, e) => PlayDaMusic(MyTimer);
            MyTimer.Start();
        }
        public static void PlayDaMusic(Timer e)
        {
            e.Dispose();
            SoundPlayer newSoundEffect = new SoundPlayer(Properties.Resources.playerwinner);
            newSoundEffect.Play();
        }
    }
}
