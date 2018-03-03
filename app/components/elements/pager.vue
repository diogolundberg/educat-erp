<template>
  <span :is="container">
    <ButtonGroup
      :options="options"
      :value="value"
      @input="$emit('input', $event)" />
  </span>
</template>

<script>
  import range from "lodash/range";

  export default {
    name: "Pager",
    props: {
      value: {
        type: Number,
        required: true,
      },
      records: {
        type: Number,
        required: true,
      },
      perPage: {
        type: Number,
        default: 20,
      },
      offset: {
        type: Number,
        default: 5,
      },
      container: {
        type: String,
        default: "span",
      },
    },
    computed: {
      options() {
        const pages = Math.ceil(this.records / this.perPage);
        const first = Math.min(Math.max(this.value - this.offset, 1), pages);
        const last = Math.min(this.value + this.offset, pages);
        return range(first, last).map(n => ({ id: n, name: `${n}` }));
      },
    },
  };
</script>
