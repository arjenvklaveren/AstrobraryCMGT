import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { environment } from '../environments/environment.development';
import { Astronomer } from '../types/Astronomer';

@Injectable({
  providedIn: 'root'
})
export class AstronomerService {
  http = inject(HttpClient);
  baseUrl = environment.apiUrl;

  getAstronomers() {
    return this.http.get<Astronomer[]>(this.baseUrl + "astronomer");
  }
}
