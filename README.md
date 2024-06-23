# CoindeskExampleApiServer
	幣率範例

## CoindeskController 

	呼叫匯率資料

## CopilotController 

	幣元資訊
	
## OtherController
	
	外觀(Facade)設計模式 / 加解密
	
###  完成項目

	1. 印出所有 API 被呼叫以及呼叫外部 API 的 request and response body log (Serilog)

	2. Error handling 處理 API response 

	3. swagger-ui

	4. 多語系設計 
		4.1 LanguageService
		4.2 CoindeskController-GetAllBPICurrentprice
		4.3 CopilotController-GetLocalizedMessage
	
	5. design pattern 實作
	(其餘模式可以參考我以前練習的-https://github.com/gguwhuWU/DesignPattern/tree/master)
		5.1 OtherController.GetFacadeSystemMethodA
		5.2 OtherController.GetFacadeSystemMethodB
		
	6. 能夠運行在 Docker
		
	7. 加解密技術應用 (AES/RSA
		7.1 OtherController-EncryptString
		7.2 OtherController-DecryptString