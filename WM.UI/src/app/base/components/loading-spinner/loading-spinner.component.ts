import { Component, Input } from "@angular/core";

@Component({
    selector: 'app-loading',
    templateUrl: './loading-spinner.component.html',
    styleUrls: ['./loading-spinner.component.scss']
})
export class LoadingSpinnerComponent {
    @Input() isFullscreen = true;
}