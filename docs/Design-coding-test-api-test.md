# coding-test-api-test プロジェクト

コーディングテスト用のAPIサーバアプリのテストプロジェクトです

## フォルダ構成

Appフォルダ配下は [coding-test-api](Design-coding-test-api.md)と同様のフォルダ構成となります。
[coding-test-api](Design-coding-test-api.md)と同位置に以下の命名規約でテストクラスを作成します。

| 命名規約                | 命名例                        |
| ----------------------- | ----------------------------- |
| {テストクラス名}Test.cs | GetInventoryControllerTest.cs |

## 設計方針

テストフレームワークには xUnit を利用します。  
テスト用のモックライブラリには Moq を利用します。

[README](README.md)
