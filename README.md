# AR Ikea Shopping Experience 🥽📦

![Unity](https://img.shields.io/badge/Unity-2022.3%2B-black?style=for-the-badge&logo=unity)
![OpenXR](https://img.shields.io/badge/OpenXR-Supported-crimson?style=for-the-badge&logo=khronos)
![Meta Quest](https://img.shields.io/badge/Meta%20Quest-Device-blue?style=for-the-badge&logo=meta)
![Platform](https://img.shields.io/badge/Platform-Android%20%28Quest%29-lightgrey?style=for-the-badge&logo=android)

**AR Ikea Shopping** is a Mixed Reality (MR) research prototype designed to explore the future of retail. Built for **Meta Quest** and **Smart Glasses**, it demonstrates how context-aware spatial computing can transform the in-store shopping experience by merging physical products with digital information layers.

> **Research Goal:** To investigate interaction patterns for AI and IoT-enabled retail environments, focusing on information density, spatial navigation, and seamless checkout flows.

---

## ✨ Key Features

This prototype implements a complete "phygital" user journey:

*   **QR Code Session Initiation:** Instantly launch the shopping companion by scanning an entry QR code.
*   **Spatial Navigation:** Real-time AR pathfinding guides users through the store to specific products or sections.
*   **Context Panels:** Gaze-activated information cards appear next to physical products, showing details, stock status, and reviews.
*   **Virtual Try-On & Compare:** specialized visualization modes to compare product variants side-by-side in 3D space.
*   **Checkout Handoff:** A seamless digital checkout flow that bridges the gap between the AR session and the physical point of sale.

## 🛠️ Tech Stack

*   **Engine:** Unity (2022.3 LTS+)
*   **XR Framework:** [OpenXR](https://www.khronos.org/openxr/) (Cross-platform compatibility)
*   **SDKs:**
    *   **Meta XR Core SDK:** For Quest-specific features (Passthrough, Anchors).
    *   **XR Interaction Toolkit (XRI):** For standardizing interactions and controller inputs.
*   **Architecture:** Custom event-driven architecture focusing on IoT data orchestration and performant spatial UI.

## 🚀 Getting Started

### Prerequisites

*   Unity Hub and Unity 2022.3 LTS (or newer)
*   Android Build Support module (OpenJDK & Android SDK/NDK installed)
*   Meta Quest 2, 3, or Pro device
*   [Meta Quest Developer Hub](https://developer.oculus.com/documentation/unity/unity-mqdh/) (recommended for easy flashing)

### Installation

1.  **Clone the repository:**
    ```bash
    git clone https://github.com/Mseymur/DesignResearchARIkeaPorototype.git
    ```
2.  **Open in Unity:**
    *   Add the project to Unity Hub.
    *   Examples are located in `Assets/Scenes/`.
3.  **Build Settings:**
    *   Switch platform to **Android**.
    *   Ensure "OpenXR" is selected in **Project Settings > XR Plug-in Management**.
    *   Verify Meta Quest feature group is checked.

4.  **Deploy:**
    *   Connect your Quest device via USB.
    *   Click **Build and Run** (or use Meta Quest Developer Hub).

## 🎮 Interaction Guide

| Action | Input (Controller/Hand) | Description |
| :--- | :--- | :--- |
| **Select / Interact** | Trigger / Pinch | Click buttons or select virtual objects. |
| **Teleport / Move** | Thumbstick / Gaze | Navigate the virtual environment (if not using room-scale). |
| **Scan QR** | Gaze Dwell | Look at a QR code for 2 seconds to trigger an event. |
| **Open Menu** | Menu Button / Palm Turn | Access the inventory or shopping list. |

## 👥 Credits

*   **Seymur Mammadov** - Lead Researcher & Developer
    *   *Concept, Prototyping, and Implementation*

## 📄 License

[License](license)

---

*[Learn more about the research behind this project.](https://mseymur.framer.website/projects/ar-ikea-shopping)*
