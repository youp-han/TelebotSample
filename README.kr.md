# TeleBotService - 텔레그램 로또 봇

이 프로젝트는 텔레그램 봇을 구현하는 C# 콘솔 애플리케이션입니다. 이 봇은 사용자에게 최신 당첨 번호, 1등 당첨금, 이번 주 추천 번호 등 로또 관련 정보를 제공하도록 설계되었습니다.

## 기능

*   **키워드 기반 상호작용:** 봇은 "로또", "/Lotto", "마이키", "Mikey", "@Mikey_봇", "봇"과 같은 특정 키워드에 반응합니다.
*   **인터랙티브 로또 조회:** `/Lotto` 키워드가 감지되면 봇은 다음을 조회할 수 있는 인터랙티브 인라인 키보드를 표시합니다:
    *   "최신당첨번호"
    *   "최신1등당첨금"
    *   "이번주번호추천"
*   **외부 데이터 가져오기:** 로또 정보는 REST 호출을 통해 외부 API에서 가져옵니다.

## 프로젝트 구조

*   `Program.cs`: 애플리케이션의 주요 진입점으로, 텔레그램 봇 이벤트 및 메시지 처리를 담당합니다.
*   `Configuration.cs`: 텔레그램 봇 토큰 및 API URL과 같은 애플리케이션별 구성을 관리합니다.
*   `Model/RestCall.cs`: 외부 서비스에 대한 REST API 호출을 처리합니다.
*   `Repository/iLottoRepository.cs`: 로또 데이터 작업에 대한 인터페이스를 정의합니다.
*   `Repository/LottoRepository.cs`: `iLottoRepository` 인터페이스를 구현하여 로또 데이터를 가져오고 처리합니다.

## 설정

TeleBotService를 설정하고 실행하려면 다음 단계를 따르십시오:

### 필수 요구 사항

*   **.NET Framework 4.6** (또는 호환되는 버전)
*   **텔레그램 봇 토큰:** 텔레그램 BotFather로부터 봇 토큰을 얻으십시오.
*   **로또 API URL:** 로또 정보를 제공하는 외부 API 엔드포인트.

### 구성

1.  **`App.config` 생성:** `TeleBotService` 프로젝트 디렉토리에 `App.config` 파일이 없으면 생성하십시오.
2.  **AppSettings 추가:** `<configuration>` 및 `<appSettings>` 태그 내에 다음 구성 설정을 `App.config` 파일에 추가하십시오:

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
    *   `YOUR_TELEGRAM_BOT_TOKEN`을 BotFather로부터 얻은 실제 토큰으로 바꾸십시오.
    *   `YOUR_LOTTO_API_URL`을 로또 데이터 API의 기본 URL로 바꾸십시오.

### 애플리케이션 실행

1.  **프로젝트 빌드:** Visual Studio에서 `TeleBotService.sln` 솔루션을 열고 프로젝트를 빌드하십시오.
2.  **실행:** `bin/Debug` (또는 `bin/Release`) 디렉토리에서 컴파일된 실행 파일을 실행하십시오.

    ```bash
    cd TeleBotService/bin/Debug
    TeleBotService.exe
    ```
    봇이 메시지를 수신하기 시작합니다.

## 사용법

봇이 실행되면:

1.  텔레그램을 열고 봇과 채팅을 시작하십시오.
2.  트리거 키워드 중 하나(예: "로또", "/Lotto", "Mikey")를 포함하는 메시지를 보내십시오.
3.  `/Lotto`를 보내면 인라인 키보드가 나타나 특정 로또 쿼리를 선택할 수 있습니다.
