# Demand Paging OS Simulation

A Windows Forms application built with .NET 8 simulating operating system memory management concepts with Demand Paging, Job allocation, Page Map Tables (PMT), and FIFO page replacement.

## Features

- **Memory & Frame Configuration**: Configure memory size, frame allocation, and job counts.
- **Job Table & Process Management**: Define jobs, sizes, and arrival sequence.
- **Page Map Table (PMT)**: Interactive page-to-frame mapping and visualization.
- **FIFO Page Replacement Simulation**: Visualizes page loading, frame allocation, and FIFO replacement behavior in real time.
- **Animated Windows Forms UI**: Built-in fade-in, sliding, and glow visual effects.

## Technologies Used

- **Language**: C#
- **Framework**: .NET 8.0 Windows Forms
- **IDE**: Visual Studio 2022

## Getting Started

### Prerequisites

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0) or higher
- Windows OS (Windows 10/11)
- Visual Studio 2022 (recommended) with *.NET desktop development* workload

### Running the Project

1. Clone the repository:
   ```bash
   git clone https://github.com/eunwia/DemandPaging_OS.git
   ```
2. Open `platech_finals_proj.sln` in Visual Studio.
3. Build and run the solution (`F5` or `Ctrl + F5`).
   Alternatively, run from CLI:
   ```powershell
   dotnet run --project platech_finals_proj/platech_finals_proj.csproj
   ```
