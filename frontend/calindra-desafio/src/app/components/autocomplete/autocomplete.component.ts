import { Component, OnInit, Output, ViewChild } from '@angular/core';
import { FormControl } from '@angular/forms';
import { debounceTime, distinctUntilChanged, filter, Observable, pipe, startWith, switchMap, tap } from 'rxjs';

import { AutocompleteService } from './services/autocomplete.service';
import { AutoCompleteProductResult } from './models/autocomplete-product-result';
import { MatAutocompleteTrigger } from '@angular/material/autocomplete';
import { Router } from '@angular/router';
import { TextToUrlPipe } from 'src/shared/pipes/text-to-url.pipe';


@Component({
  selector: 'app-autocomplete',
  templateUrl: './autocomplete.component.html',
  styleUrls: ['./autocomplete.component.css']
})
export class AutocompleteComponent implements OnInit {

  public searchControl = new FormControl();

  @ViewChild(MatAutocompleteTrigger)
  public autocomplete: MatAutocompleteTrigger | null = null;

  @Output()
  public filteredOptions$ = new Observable<AutoCompleteProductResult[]>();

  public searchQueryResult: AutoCompleteProductResult[] = [];

  constructor(
    private readonly autocompleteService: AutocompleteService,
    private readonly router: Router
  ) { }

  ngOnInit(): void {

    this.searchControl.valueChanges
      .pipe(filter(value => typeof value === 'object' && value !== null))
      .subscribe(item => this.select(item.id, item.name));

    this.filteredOptions$ = this.searchControl.valueChanges
      .pipe(filter(value => typeof value === 'string'))
      .pipe(
        startWith(''),
        debounceTime(200),
        tap(term => new TextToUrlPipe().transform(term)),
        distinctUntilChanged(),
        switchMap((term: string) => this.filter(term || ''))
      );

    this.filteredOptions$.subscribe(result => this.searchQueryResult = result);
  }

  parseDropDownSelection(item: AutoCompleteProductResult): string {
    return item && item.name;
  }

  public filter(term: string): Observable<AutoCompleteProductResult[]> {
    return this.autocompleteService.search(term);
  }

  public onKeyPressEnter(event: Event) {
    event.preventDefault();
    event.stopPropagation();
    this.autocomplete?.closePanel();
    let value = this.searchControl.value;
    if (typeof value === 'string') {
      const item = this.searchQueryResult.find(p => p.name == value);
      if (item) {
        this.select(item.id, item.name)
      } else {
        this.router.navigateByUrl(`/search/${value}`);
      }
    }
    else if (value && value.id && value.name) {
      this.select(value.id, value.name)
    }
  }

  public select(id: string, name: string) {
    name = new TextToUrlPipe().transform(name);
    this.router.navigateByUrl(`/product/${id}/${name}`);
  }
}
