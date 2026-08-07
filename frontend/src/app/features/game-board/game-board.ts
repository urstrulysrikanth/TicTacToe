import {
  Component,
  Input,
  Output,
  EventEmitter,
  OnChanges,
  SimpleChanges
} from '@angular/core';

import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-game-board',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './game-board.html',
  styleUrls: ['./game-board.scss']
})
export class GameBoardComponent implements OnChanges {

  @Input() board: string[][] = [];

  @Input() winningCells: number[] = [];

  @Output() moveSelected =
    new EventEmitter<number>();

  ngOnChanges(changes: SimpleChanges): void {
    console.log('BOARD UPDATED');
    console.log(this.board);
  }

  select(index: number): void {

    console.log('BOARD CLICK', index);

    this.moveSelected.emit(index);
  }

  isWinningCell(index: number): boolean {
    return this.winningCells.includes(index);
  }
}