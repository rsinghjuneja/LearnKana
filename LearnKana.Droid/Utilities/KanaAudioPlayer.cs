using System;
using System.Collections.Generic;

using Android.App;
using Android.Content.Res;
using Android.Media;

using LearnKana.Domain.Kana;
using LearnKana.Provider;
using LearnKana.Shared.Extensions;

namespace LearnKana.Droid.Utilities
{
    public class KanaAudioPlayer : DisposableObject
    {
        private MediaPlayer? m_MediaPlayer;

        public KanaAudioPlayer(KanaService service)
        {
            m_MediaPlayer = new MediaPlayer();

            string[] files = Application.Context.Assets?.List("Audio")
                ?? throw new NotImplementedException();

            files.ForEachElement(x =>
            {
                string name = Path.GetFileNameWithoutExtension(x);

                if (service.StandardSyllabary.TryGetValue(name, out KanaCharacter character))
                    Database.Add(character, name);
            });
        }

        public Dictionary<KanaCharacter, string> Database { get; } = [];

        public void PlayAudio(KanaCharacter character)
        {
            if (m_MediaPlayer == null)
                return;
            if (m_MediaPlayer.IsPlaying)
                m_MediaPlayer.Stop();

            m_MediaPlayer.Reset();

            string filepath = GetAudioFilename(character);
            AssetFileDescriptor? descriptor = GetFileDescriptor(filepath);
            if (descriptor == null)
                throw new NotImplementedException();
            m_MediaPlayer.SetDataSource(descriptor.FileDescriptor, descriptor.StartOffset, descriptor.Length);
            m_MediaPlayer.Prepare();
            m_MediaPlayer.Start();
        }

        public static bool HasAudioFile(KanaCharacter character)
        {
            string filepath = GetAudioFilename(character);
            AssetFileDescriptor? descriptor = GetFileDescriptor(filepath);

            if (descriptor == null)
                return false;
            return true;
        }

        public static AssetFileDescriptor? GetFileDescriptor(string filepath)
        {
            AssetFileDescriptor? descriptor = Application.Context.Assets?.OpenFd(filepath);
            return descriptor;
        }

        private static string GetAudioFilename(KanaCharacter character)
        {
            string filepath = Path.Combine("Audio", $"{character.Romaji}.mp3"); ;
            return filepath;
        }

        protected override void OnDispose()
        {
            m_MediaPlayer?.Stop();
            m_MediaPlayer?.Release();
            m_MediaPlayer = null;
        }
    }
}