﻿## はじめに
本プロジェクトはNEXTAのエンジニア採用のコーディングテストです

## プロジェクト構成

### coding-tets-model
データモデルを一式格納しているプロジェクトです。
クライアント／サーバを全てC#で統一する全体設計となっております。
フォルダ構成は以下の通りです。

|フォルダ|フォルダ|
| ---- | ---- |
|Api|Inventories|
||PurchaseOrders|
||ReceiveOrders|
|Entities|


各フォルダの説明は以下の通りです。

|フォルダ名|説明|
| ---- | ---- |
| Api | APIのインターフェース関連を格納するフォルダ |
| Entities | DBのインターフェース関連を格納するフォルダ |

### coding-test-api
コーディングテスト用のAPIサーバアプリプロジェクトです
フォルダ構成は以下の通りです。

|フォルダ|フォルダ|フォルダ|フォルダ|
| ---- | ---- |
|App|Api|Inventories|Controllers|
||||Models|
||||Services|
|||Purchases|
||||Models|
||||Services|
|||Purchases|
|||ReceiveOrders|
||||Models|
||||Services|
|||Purchases|
||Module|||
||Repositories||

各フォルダの説明は以下の通りです。

|フォルダ名|説明|
| ---- | ---- |
| App | アプリケーションフォルダ |
| Api | APIを実装しているフォルダ |
| Api 配下のフォルダ| APIをドメイン別に実装するためのフォルダ |
| ドメイン配下の Controllers | コントローラを置くフォルダ |
| ドメイン配下の Services | サービス（ビジネスロジック）を置くフォルダ |
| Modules | API開発におけるモジュール群を置くフォルダ |
| Repositories | API開発におけるDBアクセス群を置くフォルダ　※別アセンブリにするか検討中 |

### coding-tets-api-test
コーディングテスト用のAPIサーバアプリのテストプロジェクトです
Appフォルダ配下は [coding-test-api] と同様のフォルダ構成となります


