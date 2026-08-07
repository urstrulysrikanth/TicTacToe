import { Move } from './move.model';

export interface GameState {
  id: string;
  board: string[][];
  currentPlayer: string;
  status: number;
  winner: string | null;
  winningCells: number[];
  moveHistory: Move[];
  mode: number;
}