<template>
  <div class="flex">
    <span
      v-for="tab in tabs"
      :key="tab.id">
      <a
        :class="{ 'active': value === tab.id }"
        class="btn upcase input-line border-none transition"
        @click="$emit('input', tab.id)">
        {{ tab.name }}
      </a>
    </span>
  </div>
</template>

<script>
  export default {
    name: "TabHeader",
    props: {
      value: {
        type: [Number, String],
        default: null,
      },
      tabs: {
        type: Array,
        default: () => [],
      },
    },
  };
</script>
