import { Component, OnDestroy, OnInit } from "@angular/core";
import { ReplaySubject, finalize, takeUntil } from "rxjs";
import { DashboardService } from "../../services/dashboard.service";
import { MatTableDataSource } from "@angular/material/table";
import { InfoModel } from "../../models/InfoModel";

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent implements OnInit, OnDestroy {
  loading = false;
  destroy$ = new ReplaySubject<void>(1);
  donutChart: any;
  lineChart: any;
  infoData = new MatTableDataSource<InfoModel>();
  facilityId = 3;
  displayedColumns = ['product', 'amount', 'sum']

  donutData = {
    animationEnabled: true,
    theme: "light2",
    title: {
      text: "Best sells"
    },
    data: [{
      type: "doughnut",
      yValueFormatString: "#,###.##'%'",
      indexLabel: "{name}",
      dataPoints: []
    }]
  }

  lineData = {
    animationEnabled: true,
    theme: "light2",
    title: {
      text: "Popular Product Sales"
    },
    axisX: {
      valueFormatString: "D MMM"
    },
    axisY: {
      title: "Number of Sales"
    },
    toolTip: {
      shared: true
    },
    legend: {
      cursor: "pointer",
      itemclick: function (e: any) {
        if (typeof (e.dataSeries.visible) === "undefined" || e.dataSeries.visible) {
          e.dataSeries.visible = false;
        } else {
          e.dataSeries.visible = true;
        }
        e.chart.render();
      }
    },
    data: []
  }

  constructor(private dashboardService: DashboardService) { }

  ngOnInit(): void {
    this.loading = true;
    this.dashboardService.getInfoData(this.facilityId)
      .pipe(
        takeUntil(this.destroy$),
        finalize(() => this.loading = false)
      )
      .subscribe((res) => {
        this.infoData.data = res;
      })
  }

  getDonutChartInstance(chart: object) {
    this.donutChart = chart;
    this.updateDonutData();
  }

  getLineChartInstance(chart: object) {
    this.lineChart = chart;
    this.updateLineData();
  }

  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }

  updateDonutData() {
    this.loading = true;
    this.dashboardService.getDonutData(this.facilityId)
      .pipe(
        takeUntil(this.destroy$),
        finalize(() => this.loading = false)
      )
      .subscribe((res) => {
        res.forEach((item) => {
          this.donutData.data[0].dataPoints.push({
            y: Number(item.percentage),
            name: item.name,
          })
        })
        this.donutChart.render();
      });
  }

  updateLineData() {
    this.loading = true;
    this.dashboardService.getLineData(this.facilityId)
      .pipe(
        takeUntil(this.destroy$),
        finalize(() => this.loading = false)
      )
      .subscribe((res) => {
        res.forEach((item) => {
          this.lineData.data.push({
            type: "line",
            showInLegend: true,
            name: item.name,
            xValueFormatString: "MMM DD, YYYY",
            dataPoints: item.items.map((i) => {
              return {
                x: new Date(i.sellDate),
                y: i.amount
              }
            })
          })
        })
        this.lineChart.render();
        console.log(this.lineData)
      });
  }
}