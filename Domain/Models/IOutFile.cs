using System;

namespace Domain.Models
{
    public interface IOutFile
    {
        string FileName { get; }

        double Size { get; }

        dynamic Data { get; }

        DateTime DateOfCreation { get; }

        string Extension { get; }

        string Path { get; }

        void Save();
    }
}
