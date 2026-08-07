import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

import { environment } from '../../../environments/environment';
import { GameState } from '../models/game-state.model';
import { Scoreboard } from '../models/scoreboard.model';

@Injectable({
  providedIn: 'root'
})
export class GameService {

  private api = environment.apiUrl;

  constructor(private http: HttpClient) {}

  createGame(mode: number): Observable<GameState> {
    return this.http.post<GameState>(
      `${this.api}/games`,
      { mode }
    );
  }

  getGame(id: string): Observable<GameState> {
    return this.http.get<GameState>(
      `${this.api}/games/${id}`
    );
  }

  makeMove(
    gameId: string,
    player: string,
    row: number,
    column: number
  ): Observable<GameState> {

    return this.http.post<GameState>(
      `${this.api}/games/${gameId}/moves`,
      {
        player,
        row,
        column
      }
    );
  }

  undo(gameId: string): Observable<GameState> {
    return this.http.post<GameState>(
      `${this.api}/games/${gameId}/undo`,
      {}
    );
  }

  resetGame(gameId: string): Observable<GameState> {
    return this.http.post<GameState>(
      `${this.api}/games/${gameId}/reset`,
      {}
    );
  }

  getScoreboard(): Observable<Scoreboard> {
    return this.http.get<Scoreboard>(
      `${this.api}/scoreboard`
    );
  }

  resetScoreboard() {
    return this.http.post(
      `${this.api}/scoreboard/reset`,
      {}
    );
  }
}