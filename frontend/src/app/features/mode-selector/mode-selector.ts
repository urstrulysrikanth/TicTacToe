import {
  Component,
  EventEmitter,
  Output,
  Input
} from '@angular/core';

import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-mode-selector',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './mode-selector.html',
  styleUrls: ['./mode-selector.scss']
})
export class ModeSelector {

  @Input()
  selectedMode = 0;

  @Output()
  modeChanged = new EventEmitter<number>();

  selectTwoPlayer(): void {
    this.modeChanged.emit(1);
  }

  selectComputer(): void {
    this.modeChanged.emit(2);
  }
}