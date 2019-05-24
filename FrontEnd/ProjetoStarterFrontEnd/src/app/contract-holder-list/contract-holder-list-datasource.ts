import { DataSource } from '@angular/cdk/collections';
import { MatPaginator, MatSort } from '@angular/material';
import { map } from 'rxjs/operators';
import { Observable, of as observableOf, merge } from 'rxjs';

// TODO: Replace this with your own data model type
export interface ContractHolderListItem {
  name: string;
  id: number;
  rg: number;
  cpf: number;
  birthdate: number;

}

// TODO: replace this with real data from your application
const EXAMPLE_DATA: ContractHolderListItem[] = [
  {id: 1, name: 'Hydrogen', rg:123654, cpf:321654,birthdate:123,},
  {id: 2, name: 'Helium', rg:123654, cpf:321654,birthdate:123,},
  {id: 3, name: 'Lithium', rg:123654, cpf:321654,birthdate:123,},
  {id: 4, name: 'Beryllium', rg:123654, cpf:321654,birthdate:123,},
  {id: 5, name: 'Boron', rg:123654, cpf:321654,birthdate:123,},
  {id: 6, name: 'Carbon' ,rg:123654, cpf:321654,birthdate:123,},
  {id: 7, name: 'Nitrogen', rg:123654, cpf:321654,birthdate:123,},
  {id: 8, name: 'Oxygen', rg:123654, cpf:321654,birthdate:123,},
  {id: 9, name: 'Fluorine', rg:123654, cpf:321654,birthdate:123,},
  {id: 10, name: 'Neon' ,rg:123654, cpf:321654,birthdate:123,},
  {id: 11, name: 'Sodium' ,rg:123654, cpf:321654,birthdate:123,},
  {id: 12, name: 'Magnesium', rg:123654, cpf:321654,birthdate:123,},
  {id: 13, name: 'Aluminum', rg:123654, cpf:321654,birthdate:123,},
  {id: 14, name: 'Silicon', rg:123654, cpf:321654,birthdate:123,},
  {id: 15, name: 'Phosphorus', rg:123654, cpf:321654,birthdate:123,},
  {id: 16, name: 'Sulfur' ,rg:123654, cpf:321654,birthdate:123,},
  {id: 17, name: 'Chlorine' ,rg:123654, cpf:321654,birthdate:123,},
  {id: 18, name: 'Argon' ,rg:123654, cpf:321654,birthdate:123,},
  {id: 19, name: 'Potassium', rg:123654, cpf:321654,birthdate:123,},
  {id: 20, name: 'Calcium' ,rg:123654, cpf:321654,birthdate:123,},
];

/**
 * Data source for the ContractHolderList view. This class should
 * encapsulate all logic for fetching and manipulating the displayed data
 * (including sorting, pagination, and filtering).
 */
export class ContractHolderListDataSource extends DataSource<ContractHolderListItem> {
  data: ContractHolderListItem[] = EXAMPLE_DATA;

  constructor(private paginator: MatPaginator, private sort: MatSort) {
    super();
  }

  /**
   * Connect this data source to the table. The table will only update when
   * the returned stream emits new items.
   * @returns A stream of the items to be rendered.
   */
  connect(): Observable<ContractHolderListItem[]> {
    // Combine everything that affects the rendered data into one update
    // stream for the data-table to consume.
    const dataMutations = [
      observableOf(this.data),
      this.paginator.page,
      this.sort.sortChange
    ];

    // Set the paginator's length
    this.paginator.length = this.data.length;

    return merge(...dataMutations).pipe(map(() => {
      return this.getPagedData(this.getSortedData([...this.data]));
    }));
  }

  /**
   *  Called when the table is being destroyed. Use this function, to clean up
   * any open connections or free any held resources that were set up during connect.
   */
  disconnect() {}

  /**
   * Paginate the data (client-side). If you're using server-side pagination,
   * this would be replaced by requesting the appropriate data from the server.
   */
  private getPagedData(data: ContractHolderListItem[]) {
    const startIndex = this.paginator.pageIndex * this.paginator.pageSize;
    return data.splice(startIndex, this.paginator.pageSize);
  }

  /**
   * Sort the data (client-side). If you're using server-side sorting,
   * this would be replaced by requesting the appropriate data from the server.
   */
  private getSortedData(data: ContractHolderListItem[]) {
    if (!this.sort.active || this.sort.direction === '') {
      return data;
    }

    return data.sort((a, b) => {
      const isAsc = this.sort.direction === 'asc';
      switch (this.sort.active) {
        case 'name': return compare(a.name, b.name, isAsc);
        case 'id': return compare(+a.id, +b.id, isAsc);
        case 'rg': return compare (+a.rg, +b.rg, isAsc);
        case 'cpf': return compare (+a.cpf, +b.cpf, isAsc);
        case 'birthdate': return compare (+a.birthdate, +b.birthdate, isAsc);

        default: return 0;
      }
    });
  }
}

/** Simple sort comparator for example ID/Name columns (for client-side sorting). */
function compare(a, b, isAsc) {
  return (a < b ? -1 : 1) * (isAsc ? 1 : -1);
}
