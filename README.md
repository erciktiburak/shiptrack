# ShipTrack — Maritime Logistics ERP Platform

A production-ready microservices-based maritime shipment tracking platform.

## Tech Stack

**Backend:** .NET 10, C# 12, EF Core 8, SQL Server, Redis, SignalR  
**Messaging:** RabbitMQ, MassTransit  
**Frontend:** Angular 18, Signals, RxJS, PrimeNG  
**Auth:** JWT, Refresh Token Rotation  
**DevOps:** Docker, Docker Compose  

## Architecture
```
┌─────────────┐     ┌──────────────────────────────────────┐
│   Angular   │────▶│           YARP Gateway :5000          │
│  Frontend   │     └──────┬──────────┬──────────┬──────────┘
└─────────────┘            │          │          │
                           ▼          ▼          ▼
                      Order      Tracking  Notification
                      :5001       :5002      :5003
                           │          │          │
                    ┌──────▼──────────▼──────────▼──────┐
                    │  SQL Server  │  Redis  │ RabbitMQ  │
                    └───────────────────────────────────┘
```

## Services

| Service | Port | Responsibility |
|---------|------|----------------|
| Gateway | 5000 | YARP API Gateway |
| AuthService | 5004 | JWT Authentication |
| OrderService | 5001 | Shipment management |
| TrackingService | 5002 | Real-time tracking |
| NotificationService | 5003 | Event notifications |
| Frontend | 4200 | Angular 18 UI |

## Getting Started
```bash
# 1. Environment dosyasını oluştur
cp .env.example .env

# 2. Tüm servisleri başlat
docker-compose up -d

# 3. Frontend'e git
open http://localhost:4200
```

## Monitoring

- RabbitMQ Management: http://localhost:15672
- Gateway Swagger: http://localhost:5000/swagger