<template>
  <span :is="container">
    <Btn
      v-for="option in options"
      :key="option[idField]"
      :class="has(option) ? 'btn-primary' : 'bg-white black'"
      :label="option[labelField]"
      class="rounded50 sm-mr1 shadow1"
      @click="toggle(option)" />
  </span>
</template>

<script>
  import { xor } from "lodash";

  export default {
    name: "TagGroup",
    props: {
      value: {
        type: Array,
        default: () => [],
      },
      options: {
        type: Array,
        default: () => [],
      },
      idField: {
        type: String,
        default: "id",
      },
      labelField: {
        type: String,
        default: "name",
      },
      container: {
        type: String,
        default: "span",
      },
    },
    methods: {
      has(option) {
        return this.value.includes(option[this.idField]);
      },
      toggle(option) {
        this.$emit("input", xor(this.value, [option[this.idField]]));
      },
    },
  };
</script>
