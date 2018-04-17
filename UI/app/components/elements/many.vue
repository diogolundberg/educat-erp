<template>
  <div class="col-12">
    <template v-for="(item, index) in items">
      <slot
        :item="value[index]"
        :error="errorsFor(index)" />
    </template>
  </div>
</template>

<script>
  import { cloneDeep, range } from "lodash";

  export default {
    name: "Many",
    props: {
      value: {
        type: Array,
        default: () => [],
      },
      errors: {
        type: Object,
        default: () => [],
      },
      errorKey: {
        type: String,
        default: "",
      },
      default: {
        type: Object,
        default: () => ({}),
      },
      amount: {
        type: Number,
        default: null,
      },
    },
    computed: {
      items() {
        return range(0, this.amount);
      },
    },
    watch: {
      amount(value) {
        range(0, value).forEach((i) => {
          this.value[i] = this.value[i] || this.default;
        });
        this.value.splice(value);
      },
    },
    methods: {
      itemFor(index) {
        const item = this.value && this.value[index];
        return item || cloneDeep(this.default);
      },
      errorsFor(index) {
        const errors = this.errors && this.errors[`${this.errorKey}[${index}]`];
        return errors || {};
      },
    },
  };
</script>
