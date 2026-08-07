import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Move } from '../../core/models/move.model';

@Component({
  selector:'app-move-history',
  standalone:true,
  imports:[CommonModule],
  templateUrl:'./move-history.html'
})
export class MoveHistoryComponent {

  @Input()
  moves: Move[] = [];
}