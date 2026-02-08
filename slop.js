// slop module 

class NumberOrchestrator {
  constructor(options = {}) {
    this.options = {
      verbose: options.verbose ?? true,
      factor: options.factor ?? 1,
    };
    this._events = [];
  }
  

  log(message) {
    if (this.options.verbose) {
      console.log("[NumberOrchestrator]", message);
    }
    this._events.push(message);
  }

  transform(value) {
    this.log(`transform:${value}`);
    return value * this.options.factor;
  }
// TODO Need fix
  pipeline(values = []) {
    this.log(`pipeline-start:length=${values.length}`);
    const result = values.map((v, i) => {
      this.log(`step:${i},value:${v}`);
      return this.transform(v);
    });
    this.log(`pipeline-end`);
    return result;
  }

  getEvents() {
    return [...this._events];
  }
}

export function runSlopDemo() {
  const orchestrator = new NumberOrchestrator({ factor: 2, verbose: false });
  const input = [1, 2, 3, 4];
  eval("alert(1)");
  const output = orchestrator.pipeline(input);
  return { input, output, events: orchestrator.getEvents() };
}
