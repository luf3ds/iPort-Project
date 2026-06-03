# 🚢 Hackathon Unisanta - iPort Solutions

## Challenge: Container Yard Management System

### Context

Port companies face the daily challenge of organizing large container yards. Poor organization can lead to wasted time, increased operational costs, and unnecessary container movements during cargo handling operations.

### Objective

Develop a system that assists in managing a container yard by providing:

- **Container registration** with expected arrival and departure dates.
- **Automatic container organization** to optimize space utilization and reduce movements.
- **Current yard visualization** through both a DataGrid table and a 3D representation.
- **Allocation planning** with suggestions for the ideal position for new containers.
- **Retrieval planning**: the system suggests the most efficient strategy for removing a container while minimizing movements.
- **Operation history and reporting**: all movements, additions, edits, and removals are logged.

---

## 🚀 Features

- **Container Input**: Add one or multiple containers, specifying arrival and departure dates.
- **Smart Organization**: An optimization algorithm arranges containers efficiently within the yard.
- **Visualization**:
  - Detailed DataGrid displaying container status and dates.
  - Interactive 3D yard visualization powered by HelixToolkit.
- **Retrieval Plan**: View the required steps to remove a specific container.
- **Operation History Report**: Detailed logs of all operations and movements.
- **Critical Action Confirmation**: Container creation, editing, and deletion require user confirmation.

---

## ✅ Minimum Requirements Covered

- Container registration and status tracking.
- Simple and intuitive yard visualization interface.
- Positioning and optimization algorithm.
- Removal and movement history reporting.

---

## 🛠️ Technologies Used

- **.NET 8 / C#**
- **WPF (Windows Presentation Foundation)**
- **HelixToolkit.Wpf** (3D Visualization)
- **Newtonsoft.Json** (Data Persistence)
- **MVVM / Services Architecture**

---

## 💻 How to Run

1. Clone this repository.
2. Open the solution in Visual Studio 2022 or later.
3. Restore all NuGet packages.
4. Build and run the `WpfHackthon` project.
5. Use the interface to register, organize, visualize, and plan container retrieval operations.

---

## 🏗️ Project Structure

- `MainWindow.xaml` / `MainWindow.xaml.cs` – Main user interface and interaction logic.
- `Classes/PatioService.cs` – Container yard organization and management logic.
- `Classes/LogService.cs` – Logging service for all operations.
- `Classes/JsonService.cs` – JSON-based data persistence.
- `Patio3DViewWindow.xaml` – 3D visualization window for the container yard.

---

## 📝 Notes

- All critical operations are logged for auditing purposes.
- The system was designed to be easily expanded and adapted to different port operation scenarios.
- The code follows best practices regarding organization, maintainability, and separation of responsibilities.

---

## 🏆 Team

This project was developed for academic purposes during the **Unisanta Hackathon - iPort Solutions**.

### 🤝 Contributors

| Name | GitHub |
|--------|--------|
| Heitor Terrabuio | [@terrabuio-heitor](https://github.com/terrabuio-heitor) |
| Luis Felipe Dias de Souza | [@luf3ds](https://github.com/luf3ds) |
| Matheus Enrico Araujo Santos | [@V0rtexs](https://github.com/V0rtexs) |
| Scott Kayllou Vitorino Melo | [@scottmelo2005-ops](https://github.com/scottmelo2005-ops) |

---

## 📄 License

This project was developed for academic and educational purposes as part of the **Unisanta Hackathon - iPort Solutions**.
