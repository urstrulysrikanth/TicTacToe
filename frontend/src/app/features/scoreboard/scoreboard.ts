import { Component, Input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Scoreboard } from '../../core/models/scoreboard.model';

@Component({
  selector:'app-scoreboard',
  standalone:true,
  imports:[CommonModule],
  templateUrl:'./scoreboard.html'
})
export class ScoreboardComponent {

  @Input()
  score!: Scoreboard;
}