import {
  Component,
  OnInit,
  ChangeDetectorRef
} from '@angular/core';

import { CommonModule } from '@angular/common';

import { GameService } from '../../core/services/game.service';

import { GameState } from '../../core/models/game-state.model';
import { Scoreboard } from '../../core/models/scoreboard.model';
import { GameMode } from '../../core/models/game-mode.enum';
import { GameStatus } from '../../core/models/game-status.enum';

import { GameBoardComponent } from '../../features/game-board/game-board';
import { MoveHistoryComponent } from '../../features/move-history/move-history';
import { ScoreboardComponent } from '../../features/scoreboard/scoreboard';
import { ControlsComponent } from '../../features/controls/controls';
import { ModeSelector } from '../../features/mode-selector/mode-selector';

@Component({
  selector: 'app-game-page',
  standalone: true,
  imports: [
    CommonModule,
    GameBoardComponent,
    MoveHistoryComponent,
    ScoreboardComponent,
    ControlsComponent,
    ModeSelector
  ],
  templateUrl: './game-page.html',
  styleUrls: ['./game-page.scss']
})
export class GamePageComponent implements OnInit {

  game?: GameState;

  gameStarted = false;

  scoreboard: Scoreboard = {
    xWins: 0,
    oWins: 0,
    draws: 0
  };

  selectedMode = 0;

  errorMessage = '';

  constructor(
    private gameService: GameService,
    private cdr: ChangeDetectorRef
  ) { }

  ngOnInit(): void {

    // Load scoreboard only.
    // Game will be created after user selects mode.
    this.loadScoreboard();
  }

  private updateGame(response: GameState): void {

    this.game = structuredClone(response);

    console.log('UPDATED GAME');
    console.log(this.game);
  }

  onModeChanged(mode: number): void {

    this.selectedMode = mode;

    this.gameStarted = true;

    this.createGame();
  }

  createGame(): void {

    this.gameService
      .createGame(this.selectedMode)
      .subscribe({
        next: (response) => {

          this.updateGame(response);

          this.errorMessage = '';
          this.cdr.detectChanges();
        },
        error: (err) => {

          this.errorMessage =
            err?.error.eorr ??
            'Failed to create game';
            this.cdr.detectChanges();
        }
      });
  }

  loadScoreboard(): void {

    this.gameService
      .getScoreboard()
      .subscribe({
        next: (response) => {
          this.scoreboard = response;
        }
      });
  }

  onCellSelected(index: number): void {

    if (!this.game) {
      return;
    }

    if (this.game.status !== GameStatus.InProgress) {
      return;
    }

    const row = Math.floor(index / 3);
    const col = index % 3;

    this.gameService
      .makeMove(
        this.game.id,
        this.game.currentPlayer,
        row,
        col
      )
      .subscribe({
        next: (response) => {

          this.updateGame(response);

          if (
            response.status === GameStatus.Won ||
            response.status === GameStatus.Draw
          ) {
            this.loadScoreboard();
          }

          this.errorMessage = '';
          this.cdr.detectChanges();
        },
        error: (err) => {
          this.errorMessage =
            err?.error?.message ??
            'Invalid move';
            this.cdr.detectChanges();
        }
      });
  }

  undo(): void {

    if (!this.game) {
      return;
    }

    this.gameService
      .undo(this.game.id)
      .subscribe({
        next: (response) => {
          this.updateGame(response);

          this.errorMessage = '';
          this.cdr.detectChanges();
        },
        error: (err) => {
          console.log(err)
          this.errorMessage =
            err?.error.message ??
            'Undo failed!!';
            this.cdr.detectChanges();
        }
      });
  }

  resetGame(): void {

    if (!this.game) {
      return;
    }

    this.gameService
      .resetGame(this.game.id)
      .subscribe({
        next: (response) => {

          this.updateGame(response);

          this.errorMessage = '';
          this.cdr.detectChanges();
        }
      });
  }

  resetScoreboard(): void {

    this.gameService
      .resetScoreboard()
      .subscribe({
        next: () => {

          this.loadScoreboard();
        }
      });
  }

  get statusMessage(): string {

    if (!this.game) {
      return '';
    }

    if (this.game.status === GameStatus.Won) {
      return `Winner : ${this.game.winner}`;
    }

    if (this.game.status === GameStatus.Draw) {
      return 'Game Draw';
    }

    return `Current Turn : ${this.game.currentPlayer}`;
  }
}