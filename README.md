# TeleBotService - Telegram Lotto Bot

This project is a C# console application that implements a Telegram bot. The bot is designed to provide users with lotto-related information, including the latest winning numbers, the first-prize amount, and recommended numbers for the current week.

## Features

*   **Keyword-based Interaction:** The bot responds to specific keywords such as "로또", "/Lotto", "마이키", "Mikey", "@Mikey_봇", and "봇".
*   **Interactive Lotto Queries:** When `/Lotto` is detected, the bot presents an interactive inline keyboard with options to query:
    *   "최신당첨번호" (Latest winning numbers)
    *   "최신1등당첨금" (Latest 1st prize amount)
    *   "이번주번호추천" (Recommended numbers for this week)
*   **External Data Fetching:** Lotto information is fetched from an external API via REST calls.

## Project Structure

*   `Program.cs`: The main entry point of the application, handling Telegram bot events and message processing.
*   `Configuration.cs`: Manages application-specific configurations, such as the Telegram Bot Token and API URLs.
*   `Model/RestCall.cs`: Handles making REST API calls to external services.
*   `Repository/iLottoRepository.cs`: Defines the interface for lotto data operations.
*   `Repository/LottoRepository.cs`: Implements the `iLottoRepository` interface, fetching and processing lotto data.

## Setup

To set up and run the TeleBotService, follow these steps:

### Prerequisites

*   **.NET Framework 4.6** (or compatible version)
*   **Telegram Bot Token:** Obtain a bot token from BotFather on Telegram.
*   **Lotto API URL:** An external API endpoint that provides lotto information.

### Configuration

1.  **Create `App.config`:** In the `TeleBotService` project directory, create a file named `App.config` if it doesn't already exist.
2.  **Add AppSettings:** Add the following configuration settings to your `App.config` file within the `<configuration>` and `<appSettings>` tags:

    ```xml
    <?xml version="1.0" encoding="utf-8" ?>
    <configuration>
        <startup>
            <supportedRuntime version="v4.0" sku=".NETFramework,Version=v4.6" />
        </startup>
        <appSettings>
            <add key="TelegramBot_Key" value="YOUR_TELEGRAM_BOT_TOKEN" />
            <add key="Url_address" value="YOUR_LOTTO_API_URL" />
        </appSettings>
    </configuration>
    ```
    *   Replace `YOUR_TELEGRAM_BOT_TOKEN` with the actual token you obtained from BotFather.
    *   Replace `YOUR_LOTTO_API_URL` with the base URL of your lotto data API.

### Running the Application

1.  **Build the Project:** Open the `TeleBotService.sln` solution in Visual Studio and build the project.
2.  **Execute:** Run the compiled executable from the `bin/Debug` (or `bin/Release`) directory.

    ```bash
    cd TeleBotService/bin/Debug
    TeleBotService.exe
    ```
    The bot will start listening for messages.

## Usage

Once the bot is running:

1.  Open Telegram and start a chat with your bot.
2.  Send a message containing one of the trigger keywords (e.g., "로또", "/Lotto", "Mikey").
3.  If you send `/Lotto`, an inline keyboard will appear, allowing you to select specific lotto queries.