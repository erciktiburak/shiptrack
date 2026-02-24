cat > README.md << 'EOF'
# ShipTrack — Maritime Logistics ERP Platform

A microservices-based maritime shipment tracking platform built with .NET 10, Angular 18, RabbitMQ, and Docker.

## Architecture
```
shiptrack/
├── services/
│   ├── ShipTrack.OrderService        # Shipment order management
│   ├── ShipTrack.TrackingService     # Real-time location & status tracking
│   └── ShipTrack.NotificationService # Event-driven notifications
├── gateway/
│   └── ShipTrack.Gateway             # YARP API Gateway
├── shared/
│   └── ShipTrack.Shared              # Shared models & events
├── frontend/
│   └── shiptrack-ui                  # Angular 18 frontend
└── docker-compose.yml
```

## Tech Stack

**Backend:** .NET 10, C# 12, EF Core 8, SQL Server, Redis, SignalR  
**Messaging:** RabbitMQ, MassTransit, Saga Pattern  
**Frontend:** Angular 18, Signals, RxJS, NgRx, PrimeNG  
**DevOps:** Docker, Kubernetes, Helm, Jenkins  
**Observability:** Prometheus, Grafana, OpenTelemetry  

## Services

| Service | Port | Responsibility |
|---------|------|----------------|
| OrderService | 5001 | Create & manage shipment orders |
| TrackingService | 5002 | Track shipment location & status |
| NotificationService | 5003 | Send real-time notifications |
| Gateway | 5000 | API Gateway (YARP) |

## Getting Started
```bash
docker-compose up -d
```

## Status
In active development
EOF