import { Observable } from 'rxjs';

import { MethodResult } from 'src/shared/results/method-result';
import { Command } from '../inputs/command';

export interface CommandHandler<TCommand extends Command>  {
    execute(command: TCommand): Observable<MethodResult>;
}