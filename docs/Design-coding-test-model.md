# coding-test-model プロジェクト

データモデルを一式格納しているプロジェクトです。  

## フォルダ構成

```text
Root:
├─Api
│  ├─Inventories
│  ├─PurchaseOrders
│  └─ReceiveOrders
└─Entities
```

各フォルダの説明は以下の通りです。

| フォルダ名 | 説明                                        |
| ---------- | ------------------------------------------- |
| Api        | APIのインターフェース関連を格納するフォルダ |
| Entities   | DBのインターフェース関連を格納するフォルダ  |

## 設計方針

モデルを別アセンブリに切り出してクライアント／サーバの双方から参照します。
こうすることでクライアント／サーバ間でのデータ解釈をDRYにします。

[README](README.md)
