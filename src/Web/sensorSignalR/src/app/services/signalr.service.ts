import { Injectable } from '@angular/core';
import * as signalR from "@microsoft/signalr"
import { ChartModel } from './chartmodel';


@Injectable({
  providedIn: 'root'
})
export class SignalrService {
  public data!: ChartModel[];
  public bradcastedData!: ChartModel[];
  private hubConnection!: signalR.HubConnection;
  
    public startConnection = () => {
      this.hubConnection = new signalR.HubConnectionBuilder()
                              .withUrl('https://localhost:5001/chart')
                              .build();
      this.hubConnection
        .start()
        .then(() => console.log('Connection started'))
        .catch((err: string) => console.log('Error while starting connection: ' + err))
    }
    
    public addTransferChartDataListener = () => {
      this.hubConnection.on('transferchartdata', (data: ChartModel[]) => {
        this.data = data;
        console.log(data);
      });
    }

    public broadcastChartData = () => {
      const data = this.data.map(m => {
        const temp = {
          data: m.data,
          label: m.label
        }
        return temp;
      });

      this.hubConnection.invoke('broadcastchartdata', data)
      .catch((err: any) => console.error(err));
    }

    public addBroadcastChartDataListener = () => {
      this.hubConnection.on('broadcastchartdata', (data: ChartModel[]) => {
        this.bradcastedData = data;
      })
    }
}