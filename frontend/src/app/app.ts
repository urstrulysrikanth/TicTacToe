import { Component } from '@angular/core';
import { GamePageComponent } from './pages/game-page/game-page';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [GamePageComponent],
  templateUrl: './app.html'
})
export class AppComponent {
}