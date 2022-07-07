using System;
using System.Drawing;

namespace Domain.Models
{
    public interface IOutFile
    {
        string FileName { get; }

        double Size { get; }

        DateTime DateOfCreation { get; }

        string Extension { get; }

        string Path { get; }

        void Save();
    }
}
