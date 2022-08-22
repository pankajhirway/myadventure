import { HttpClient } from '@angular/common/http';
import { Injectable, isDevMode } from '@angular/core';
import { Adventure, GameSession } from './node.model';
import { catchError, Observable, of, tap } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AdventureService {

  baseURI: string =  isDevMode() ? "http://localhost:8080" : "http://localhost:8080"

  constructor(private httpclient: HttpClient) { }

  Adventures: Adventure[] = [];

  getAdventuresV1(): Observable<Adventure[]> {
    return this.httpclient.get<Adventure[]>(this.baseURI+ "/v1/Adventure").pipe(
      tap(adventures => {console.log("Fetching Adventure Lists !!");this.Adventures = adventures;})
    )
  }

  startAdventuresV1(adventureId: string): Observable<GameSession> {
     var body = {
      playerName: "Pankaj",
      adventureId: adventureId
     }
    return this.httpclient.post<GameSession>(this.baseURI+ "/v1/game",body);
  }

  recordStep(sessionId: string,stepId: string, choice: string): Observable<GameSession> {
    var body = {
      id: sessionId,
      sessionId: sessionId,
      stepId: stepId,
      choice: choice
    }
   return this.httpclient.put<GameSession>(this.baseURI+ "/v1/game",body);
 }

  /**
   * Handle Http operation that failed.
   * Let the app continue.
   *
   * @param operation - name of the operation that failed
   * @param result - optional value to return as the observable result
   */
   private handleError<T>(operation = 'operation', result?: T) {
    return (error: any): Observable<T> => {

      // TODO: send the error to remote logging infrastructure
      console.error(error); // log to console instead

      // TODO: better job of transforming error for user consumption
      this.log(`${operation} failed: ${error.message}`);

      // Let the app keep running by returning an empty result.
      return of(result as T);
    };
  }

  /** Log a HeroService message with the MessageService */
  private log(message: string) {
    console.log(message);
  }
}
