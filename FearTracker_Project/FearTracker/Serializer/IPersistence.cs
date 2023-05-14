using System;

namespace GameTracker
{
    interface IPersistence
    {
        void InitPersistance();
        void send(TrackerEvent e);
        void flush();
        void close();

    }
}
