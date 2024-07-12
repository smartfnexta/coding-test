# Design-coding-test-api プロジェクト

コーディングテスト用のAPIサーバアプリプロジェクトです

## フォルダ構成

```text
Root:
└─App
   ├─Api
   │  ├─Inventories
   │  │  ├─Controllers
   │  │  └─Services
   │  ├─Purchases
   │  │  ├─Controllers
   │  │  └─Services
   │  └─ReceiveOrders
   │      ├─Controllers
   │      └─Services
   ├─Modules
   └─Repositories
```

各フォルダの説明は以下の通りです。

| フォルダ名                 | 説明                                                                   |
| -------------------------- | ---------------------------------------------------------------------- |
| App                        | アプリケーションフォルダ                                               |
| Api                        | APIを実装しているフォルダ                                              |
| Api 配下のフォルダ         | APIをドメイン別に実装するためのフォルダ                                |
| ドメイン配下の Controllers | コントローラを置くフォルダ                                             |
| ドメイン配下の Services    | サービス（ビジネスロジック）を置くフォルダ                             |
| Modules                    | API開発におけるモジュール群を置くフォルダ                              |
| Repositories               | API開発におけるDBアクセス群を置くフォルダ　※別アセンブリにするか検討中 |

## 設計方針

* API は REST を採用しています。  
* 「コントロール・サービス・リポジトリ」3層レイヤードアーキテクチャを採用しています。  
* 各レイヤー間はDIコンテナでのコンストラクタインジェクションを採用しています。

[README](README.md)
